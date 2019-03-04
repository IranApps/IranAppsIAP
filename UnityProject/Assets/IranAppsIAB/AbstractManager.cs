using UnityEngine;

namespace IranAppsPlugin
{
    public abstract class AbstractManager : MonoBehaviour
    {
        private static GameObject mIranAppsGameObject;
        private static MonoBehaviour mIranAppsGameObjectMonobehaviourRef;

        private const string IranAppsObjectName = "IranAppsIABPlugin";

        public static GameObject getIranAppsManagerGameObject()
        {
            if (mIranAppsGameObject != null)
                return mIranAppsGameObject;

            mIranAppsGameObject = GameObject.Find(IranAppsObjectName);
            if (mIranAppsGameObject == null)
            {
                mIranAppsGameObject = new GameObject(IranAppsObjectName);
                DontDestroyOnLoad(mIranAppsGameObject);
            }

            return mIranAppsGameObject;
        }

        public static void initialize(System.Type type)
        {
            try
            {
                if ((FindObjectOfType(type) as MonoBehaviour) != null)
                    return;

                GameObject managerGameObject = getIranAppsManagerGameObject();
                GameObject gameObject = new GameObject(type.ToString());
                gameObject.AddComponent(type);
                gameObject.transform.parent = managerGameObject.transform;
            }
            catch (UnityException ex)
            {
                string str1 = "It looks like you have the " + type + " on a GameObject in your scene. Our prefab-less manager system does not require the " + type
                    + " to be on a GameObject.\nIt will be added to your scene at runtime automatically for you. Please remove the script from your scene." + ex;

                Debug.LogWarning(str1);
            }
        }

        private void Awake()
        {
            gameObject.name = GetType().ToString();
            DontDestroyOnLoad(this);
        }
    }
}