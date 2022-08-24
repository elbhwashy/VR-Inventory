using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GVRI{

    public class Slot : MonoBehaviour
    {
        [Header("Grib Controller References")]
        public InputActionReference RightGribController_Refrenece = null;
        public InputActionReference LeftGribController_Refrenece = null;
        //the thing containing the data
        [SerializeField, HideInInspector]
        private CoreSlot _coreSlot = new CoreSlot();
        public CoreSlot CoreSlot
        {
            get => _coreSlot;
            set
            {
                if (_coreSlot != null) // there is a slot base slot already attached to this slot. remove its references
                    _coreSlot.subscribers.Remove(NotifySlotChange);

                _coreSlot = value;
                if (_coreSlot != null)
                {
                    _coreSlot.subscribers.Add(NotifySlotChange);
                    NotifySlotChange(_coreSlot);
                }

            }
        }

        //public float distance = 0.1f; future to get rid of other components
        public float pushForce = 1.0f;
        (GameObject, ItemInfo) preview;
        public float previewRotationSpeed = 1.0f;
        public float previewSizeFactor = 0.8f;

        private bool isControllerIn = false;  

        public int SlotID { get { return slotID; } }
        [SerializeField]
        private int slotID;

        ~Slot()
        {
            _coreSlot.subscribers.Remove(NotifySlotChange);
        }


        private void OnEnable()
        {
            // set up input refrenece
            RightGribController_Refrenece.action.started += OnClickGrib;
            LeftGribController_Refrenece.action.started += OnLeftClickGrib;
        }

        private void OnDisable()
        {
            RightGribController_Refrenece.action.started -= OnClickGrib;
            LeftGribController_Refrenece.action.started -= OnLeftClickGrib;
        } 


        private void OnClickGrib(InputAction.CallbackContext obj)
        {
            if (isControllerIn)
            {
                Debug.Log("rightGrib;");
                _coreSlot.subscribers.Remove(NotifySlotChange);
            }
        }
        
        private void OnLeftClickGrib(InputAction.CallbackContext obj)
        {
            if (isControllerIn)
            {
                Debug.Log("rightGrib;");
                _coreSlot.subscribers.Remove(NotifySlotChange);
            }
        }

        void Awake()
        {
            if (_coreSlot == null)
                CoreSlot = new CoreSlot();
            _coreSlot.subscribers.Add(NotifySlotChange);
        }

        /// Start is called before the first frame update
        void Start()
        {
            NotifySlotChange(_coreSlot);
            if (CoreSlot.ItemInfo != null && preview.Item1 == null)
            {//make a preview of the item in storage if there were none before
                //should only contain graphical elements
                preview.Item1 = CreatePreviewCopy(CoreSlot.ItemInfo.prefab);

            }

            Utils.AddSphereTriggerColliderIfNotAvailable(gameObject);
        }

        public void SetItem(int itemID, uint itemCount)
        {
            Items items = FindObjectOfType<Items>();

            ItemInfo newItem = items.itemInfos.Find(x => x.ID == itemID);

            if (newItem)
            {
                CoreSlot.ItemInfo = newItem;
                CoreSlot.ItemCount = itemCount;
            }
        }

        void Update()
        {//slowly rotate itemPreview around
            if (preview.Item1)
            {
                preview.Item1.transform.LookAt(
                    preview.Item1.transform.position + Vector3.up,
                    new Vector3(Mathf.Cos(Time.time), 0.0f, Mathf.Sin(Time.time))
                );
            }
        }

        private void NotifySlotChange(CoreSlot s)
        {
            ItemInfo ii = s.ItemInfo;
            if (preview.Item2 != ii)
            {
                Destroy(preview.Item1);
                preview.Item2 = ii;
                if (ii != null)
                {
                    //make a preview of the item in storage if there were none before
                    //should only contain graphical elements
                    preview.Item1 = CreatePreviewCopy(ii.prefab);
                }
            }
        }

        /// Creates a preview of the GameObject specified in the ItemInfo
        GameObject CreatePreviewCopy(GameObject original)
        {
            Debug.Log("here");
            GameObject preview = Instantiate(original, transform.position, transform.rotation);
            Debug.Log(preview.name + "here"); 

            //remove all components that are not used for visuals
            var components = preview.GetComponents<Component>();
            foreach (Component comp in components)
            {
                if (!(comp is MeshRenderer) &&
                    !(comp is MeshFilter) &&
                    !(comp is Transform)
                )
                {
                    Destroy(comp);
                }
            }

            Rigidbody rb = preview.GetComponent<Rigidbody>();
            rb.isKinematic = true;

            //set parent
            preview.transform.parent = transform;

            //resize a little
            preview.transform.localScale *= previewSizeFactor;

            return preview;
        }

        /// Attempt to store a GameObject and returns true if succeeded
        public bool Store(GameObject go)
        {
            if (!go) return false; //gameobject does not exist

            Item item = go.GetComponent<Item>();
            if (!item)
            {//not an item, don't care
                return false;
            }

            //if (go.transform.parent != null)
            //{ //this item is being held, insertion probably not intended
            //    return false;
            //}

            ItemInfo itemInfo = item.itemInfo;
            if (CoreSlot.Add(itemInfo, 1) == 1)
            {//sucessfully moved object into inventorySlot
                //remove the item with gameobject
                Destroy(go);
                go.SetActive(false); //disable anything till deleted (OnTriggerStay gets called multiple times before OnDestroy)
                return true;
            }
            //push the item out of the inventory
            item.push((go.transform.position - transform.position) * pushForce);
            return false;
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            Store(other.gameObject);
            Debug.Log("dfsdv" + other.tag);
            Debug.Log( "xcv"+ other.name);

            if (other.tag == "Hand")
            {
                Debug.Log(other.name + "ControllerIn");
                isControllerIn = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            Store(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Hand")
            {
                Debug.Log(other.name + "ControllerOut");
                Debug.Log( "xcv"+ other.name);
                isControllerIn = false;
            }
        }
    }
}