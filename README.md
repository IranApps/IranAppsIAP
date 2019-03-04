## BUILD INSTRUCTION

To build `IranAppsIAB.jar` from the java source code:

### first approach :

1. Open a command prompt
2. Navigate to JavaPlugin folder
3. Type `gradlew createJar`
4. After the build is succeeded you can find `IranAppsIAB.jar` in the build folder

### second approach :

1. Open JavaPlugin Project in Android Studio
2. Sync Gradle to download needed dependencies
3. Open Gradle Tab from Top-Right side of android studio.
4. Open Tasks
5. Open IranApps Group
6. Execute createJar
7. After the build is succeeded you can find `IranAppsIAB.jar` in the build folder

## INSIDE UNITY PROJECT

This plugin has not any prefab to use, it will manage the required objects.

The `IranAppsIAB` is the interface that let you call billing functions, all methods are static so is it not required to instantiate this class. Before calling any other function try to initialize the plugin by calling the `init` with the public key provided by IranApps developer portal.

This call will check to see if billing is supported and fire the `billingSupportedEvent` if it is. If billing is not supported the `billingNotSupportedEvent` will fire and you should not call any other methods.

There is `IABEventManager` class that you can subscribe to all plugin events.

After you find out that the billing is supported, you can call `queryInventory` by providing all of your available skus. When the `queryInventorySucceededEvent` fires it will contain a list of all the current purchases, subscriptions and a list of all your project sku details. You can use this information to setup your store. The list is also handy when you want to consume a purchase. Any `BazaarPurchases` returned are available for consumption.

Add the plugin activity in the Application section of the `AndroidManifest.xml`:

    <activity android:name="com.iranapps.IranAppsIABProxyActivity" android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen" />

Also add the required permissions to your manifest:

    <uses-permission android:name="android.permission.INTERNET" />
    <uses-permission android:name="ir.tgbs.iranapps.permission.BILLING"/>

# Methods

IranAppsIAB
Methods are inside `IranAppsIAB` class.

```csharp
// Initializes the billing system
public static void init(string publicKey)

// Get current version of plugin
public static string GetVersion()

// Toggles high detail logging on/off
public static void enableLogging(bool shouldEnable)

// Unbinds and shuts down the billing service
public static void unbindService()

// Returns whether subscriptions are supported on the current device
public static bool areSubscriptionsSupported()

// Sends a request to get all completed purchases and product information as setup in the IranApps dashboard about the provided skus (requires user to be logged in otherwise you will get error)
public static void queryInventory(string[] skus)

// Sends a request to get all product information as setup in the IranApps portal about the provided skus (do not required user to be loggedin)
public static void querySkuDetails(string[] skus)

// Sends a request to get all completed purchases (requires user to be logged in otherwise you will get error)
public static void queryPurchases()

// Sends out a request to purchase the product
public static void purchaseProduct(string sku)
public static void purchaseProduct(string sku, string developerPayload)

// Sends out a request to consume the product
public static void consumeProduct(string sku)
// Sends out a request to consume all of the provided products
public static void consumeProducts(string[] skus)
```

# Events

You can access events from `IABEventManager` class.

```csharp

// Fired after init is called when billing is supported on the device
public static event Action billingSupportedEvent;

// Fired after init is called when billing is not supported on the device
public static event Action<string> billingNotSupportedEvent;

// Fired when the inventory and purchase history query has returned
public static event Action<List<IranAppsPurchase>, List<IranAppsSkuInfo>> queryInventorySucceededEvent;

// Fired when the inventory and purchase history query fails
public static event Action<string> queryInventoryFailedEvent;

// Fired when the SkuDetails query has returned
public static event Action<List<IranAppsSkuInfo>> querySkuDetailsSucceededEvent;

// Fired when the SkuDetails query fails
public static event Action<string> querySkuDetailsFailedEvent;

// Fired when the purchase history query has returned
public static event Action<List<IranAppsPurchase>> queryPurchasesSucceededEvent;

// Fired when the purchase history query fails
public static event Action<string> queryPurchasesFailedEvent;

// Fired when a purchase succeeds
public static event Action<IranAppsPurchase> purchaseSucceededEvent;

// Fired when a purchase fails
public static event Action<string> purchaseFailedEvent;

// Fired when a call to consume a product succeeds
public static event Action<IranAppsPurchase> consumePurchaseSucceededEvent;

// Fired when a call to consume a product fails
public static event Action<string> consumePurchaseFailedEvent;

```
