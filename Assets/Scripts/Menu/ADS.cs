//Código creado por Aarón Angulo

using System;
using UnityEngine;
using UnityEngine.Purchasing;

// Placing the Purchaser class in the CompleteProject namespace allows it to interact with ScoreManager, one of the existing Survival Shooter scripts.
namespace CompleteProject
{

    // Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
    public class ADS : MonoBehaviour, IStoreListener
    {
        private static IStoreController m_StoreController;                                                                  // Reference to the Purchasing system.
        private static IExtensionProvider m_StoreExtensionProvider;                                                         // Reference to store-specific Purchasing subsystems.

        // Product identifiers for all products capable of being purchased: "convenience" general identifiers for use with Purchasing, and their store-specific identifier counterparts 
        // for use with and outside of Unity Purchasing. Define store-specific identifiers also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)
       
        private static string Ads1Google = "ads_costo_12";
        private static string Ads2Google = "ads_costo_24";
        private static string Ads3Google = "ads_costo_36";
        private static string Ads4Google = "ads_costo_48";
        private static string Ads5Google = "ads_costo_60";

        private AnimAds animaciones;
        
        public void EliminarAds(string ID)
        {
            //si es 1 entonces ya está comprado
            if ((m_StoreController.products.WithStoreSpecificID(Ads1Google).hasReceipt) || (m_StoreController.products.WithStoreSpecificID(Ads2Google).hasReceipt) || (m_StoreController.products.WithStoreSpecificID(Ads3Google).hasReceipt) || (m_StoreController.products.WithStoreSpecificID(Ads4Google).hasReceipt) || (m_StoreController.products.WithStoreSpecificID(Ads5Google).hasReceipt))
                PlayerPrefs.SetInt("Ads", 1);
            else
                BuyProductID(ID);
        }

        public void VerificarTicket()
        {
            if ((m_StoreController.products.WithStoreSpecificID(Ads1Google).hasReceipt) || (m_StoreController.products.WithStoreSpecificID(Ads2Google).hasReceipt) || (m_StoreController.products.WithStoreSpecificID(Ads3Google).hasReceipt) || (m_StoreController.products.WithStoreSpecificID(Ads4Google).hasReceipt) || (m_StoreController.products.WithStoreSpecificID(Ads5Google).hasReceipt))
                PlayerPrefs.SetInt("Ads", 1);
            Debugger.Escribir("Ads: " + PlayerPrefs.GetInt("Ads"));
        }

        void Start()
        {
            animaciones = GetComponent<AnimAds>();
            // If we haven't set up the Unity Purchasing reference
            if (m_StoreController == null)
            {
                // Begin to configure our connection to Purchasing
                InitializePurchasing();
            }

        }

        public void Aviso()
        {
            VerificarTicket();
            if ((PlayerPrefs.GetInt("Ads") == 0) && (!animaciones.precios))
            {
                string[] s = new Dialogos().AvisoNoAds();
                Advisor advisor = gameObject.AddComponent(typeof(Advisor)) as Advisor;
                advisor.Crear(s[0], s[1]);
                animaciones.Precios();
            }
            else
            if ((PlayerPrefs.GetInt("Ads") == 1) && (!animaciones.precios))
            {
                string[] s = new Dialogos().AvisoAds();
                (gameObject.AddComponent(typeof(Advisor)) as Advisor).Crear(s[0], s[1]);
            }
            else
            if(animaciones.precios)
            {
                animaciones.Precios();
            }
        }

        public void InitializePurchasing()
        {
            // If we have already connected to Purchasing ...
            if (IsInitialized())
            {
                // ... we are done here.
                return;
            }

            // Create a builder, first passing in a suite of Unity provided stores.
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            // Add a product to sell / restore by way of its identifier, associating the general identifier with its store-specific identifiers.
            builder.AddProduct(Ads1Google, ProductType.NonConsumable, new IDs() { { Ads1Google, GooglePlay.Name } });
            builder.AddProduct(Ads2Google, ProductType.NonConsumable, new IDs() { { Ads2Google, GooglePlay.Name } });
            builder.AddProduct(Ads3Google, ProductType.NonConsumable, new IDs() { { Ads3Google, GooglePlay.Name } });
            builder.AddProduct(Ads4Google, ProductType.NonConsumable, new IDs() { { Ads4Google, GooglePlay.Name } });
            builder.AddProduct(Ads5Google, ProductType.NonConsumable, new IDs() { { Ads5Google, GooglePlay.Name } });

            UnityPurchasing.Initialize(this, builder);
        }


        private bool IsInitialized()
        {
            // Only say we are initialized if both the Purchasing references are set.
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }

        void BuyProductID(string productId)
        {
            // If the stores throw an unexpected exception, use try..catch to protect my logic here.
            try
            {
                // If Purchasing has been initialized ...
                if (IsInitialized())
                {
                    // ... look up the Product reference with the general product identifier and the Purchasing system's products collection.
                    Product product = m_StoreController.products.WithID(productId);

                    // If the look up found a product for this device's store and that product is ready to be sold ... 
                    if (product != null && product.availableToPurchase)
                    {
                        Debugger.Escribir(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
                        m_StoreController.InitiatePurchase(product);
                    }
                    // Otherwise ...
                    else
                    {
                        // ... report the product look-up failure situation  
                        Debugger.Escribir("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                    }
                }
                // Otherwise ...
                else
                {
                    // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or retrying initiailization.
                    Debugger.Escribir("BuyProductID FAIL. Not initialized.");
                }
            }
            // Complete the unexpected exception handling ...
            catch (Exception e)
            {
                // ... by reporting any unexpected exception for later diagnosis.
                Debugger.Escribir("BuyProductID: FAIL. Exception during purchase. " + e);
            }
        }


        // Restore purchases previously made by this customer. Some platforms automatically restore purchases. Apple currently requires explicit purchase restoration for IAP.
        public void RestorePurchases()
        {
            // If Purchasing has not yet been set up ...
            if (!IsInitialized())
            {
                // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
                Debugger.Escribir("RestorePurchases FAIL. Not initialized.");
                return;
            }

            // If we are running on an Apple device ... 
            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                // ... begin restoring purchases
                Debugger.Escribir("RestorePurchases started ...");

                // Fetch the Apple store-specific subsystem.
                var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
                // Begin the asynchronous process of restoring purchases. Expect a confirmation response in the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
                apple.RestoreTransactions((result) => {
                    // The first phase of restoration. If no more responses are received on ProcessPurchase then no purchases are available to be restored.
                    Debugger.Escribir("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
                });
            }
            // Otherwise ...
            else
            {
                // We are not running on an Apple device. No work is necessary to restore purchases.
                Debugger.Escribir("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            }
        }


        //  
        // --- IStoreListener
        //

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            // Purchasing has succeeded initializing. Collect our Purchasing references.
            Debugger.Escribir("OnInitialized: PASS");

            // Overall Purchasing system, configured with products for this application.
            m_StoreController = controller;

            // Store specific subsystem, for accessing device-specific store features.
            m_StoreExtensionProvider = extensions;
        }


        public void OnInitializeFailed(InitializationFailureReason error)
        {
            // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
            Debugger.Escribir("OnInitializeFailed InitializationFailureReason:" + error);
        }

        
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            /*
            if ((String.Equals(args.purchasedProduct.definition.id, Ads1Google, StringComparison.Ordinal) || (String.Equals(args.purchasedProduct.definition.id, Ads2Google, StringComparison.Ordinal)) || (String.Equals(args.purchasedProduct.definition.id, Ads3Google, StringComparison.Ordinal)) || (String.Equals(args.purchasedProduct.definition.id, Ads4Google, StringComparison.Ordinal)) || (String.Equals(args.purchasedProduct.definition.id, Ads5Google, StringComparison.Ordinal))))
            {
                PlayerPrefs.SetInt("Ads", 1);
                Debugger.Escribir("Comprado");
            }*/
            PlayerPrefs.SetInt("Ads", 1);
            return PurchaseProcessingResult.Complete;
        }


        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing this reason with the user.
            
            Debugger.Escribir(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
            
        }

        
    }
}
