using UnityEngine;

using IranAppsPlugin;

public class IABTestUI : MonoBehaviour
{
//#if UNITY_ANDROID

    // Enter all the available skus from the IranApps Developer Portal in this array so that item information can be fetched for them
    string[] skus = { "test1"
                , "test2"
                , "test3"};

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10f, 10f, Screen.width - 15f, Screen.height - 15f));
        GUI.skin.button.fixedHeight = 50;
        GUI.skin.button.fontSize = 20;

        if (Button("Initialize IAB"))
        {
            var key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDjCzpWb0ku5seGQ3CRpEeWgEEOeXS/PYP8kl7vwJDR4LXG6VqYYbnzCjjA1M087UoFfuxYbuQRwWhMhh+JuhP1sr8lH3ysm9WVke+w7LIaqnAgASOIq+hQPycFlgcixRAFK/0j5P3rZjg4mu7bugmV0v0nMZnNzvlnN9JrtEm0KwIDAQAB"; // Please change this RSA-KEY
            IranAppsIAB.init(key);
        }

        if (Button("Query Inventory"))
        {
            IranAppsIAB.queryInventory(skus);
        }

        if (Button("Query SkuDetails"))
        {
            IranAppsIAB.querySkuDetails(skus);
        }

        if (Button("Query Purchases"))
        {
            IranAppsIAB.queryPurchases();
        }

        if (Button("Are subscriptions supported?"))
        {
            Debug.Log("subscriptions supported: " + IranAppsIAB.areSubscriptionsSupported());
        }

        if (Button("Purchase Product Test1"))
        {
            IranAppsIAB.purchaseProduct("test1");
        }

        if (Button("Purchase Product Test2"))
        {
            IranAppsIAB.purchaseProduct("test2");
        }

        if (Button("Consume Purchase Test1"))
        {
            IranAppsIAB.consumeProduct("test1");
        }

        if (Button("Consume Purchase Test2"))
        {
            IranAppsIAB.consumeProduct("test2");
        }

        if (Button("Consume Multiple Purchases"))
        {
            var skus = new string[] { "test1", "test2" };
            IranAppsIAB.consumeProducts(skus);
        }

        if (Button("Test Unavailable Item"))
        {
            IranAppsIAB.purchaseProduct("unavailable");
        }

        if (Button("Purchase Monthly Subscription"))
        {

            // SUBSCRIPTION NOT SUPPORTED
        }

        if (Button("Purchase Annually Subscription"))
        {
            // SUBSCRIPTION NOT SUPPORTED
        }

        if (Button("Enable High Details Logs"))
        {
            IranAppsIAB.enableLogging(true);
        }

        GUILayout.EndArea();
    }

    bool Button(string label)
    {
        GUILayout.Space(5);
        return GUILayout.Button(label);
    }

//#endif

}

