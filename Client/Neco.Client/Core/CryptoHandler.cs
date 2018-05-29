using libsignal.ecc;
using System;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Neco.Client.Core
{
    public class CryptoHandler
    {
        private IDataStore dataStore;
        private ECKeyPair keyPair;
        private const string securityString = "NeCo";

        public CryptoHandler(object context, IDataStore dataStore_ = null)
        {
            dataStore = dataStore_ ?? DependencyService.Get<IDataStore>();

            LoadKeyIfAvailable(dataStore, context);
            if(keyPair == null)
            {
                GenerateKey(dataStore, context);
            }
        }

        private void LoadKeyIfAvailable(IDataStore dataStore, object context)
        {
            string privateKey = dataStore.GetString(context, "privateKey");
            string publicKey = dataStore.GetString(context, "publicKey");

            if (privateKey != null && privateKey != null)
            {
                byte[] publicKeyBytes = Convert.FromBase64String(publicKey);
                if (publicKeyBytes.Length > 1 && publicKeyBytes[0] == (byte)Curve.DJB_TYPE)
                {
                    DjbECPublicKey pubKey = new DjbECPublicKey(publicKeyBytes.Skip(1).ToArray());
                    DjbECPrivateKey privKey = new DjbECPrivateKey(Convert.FromBase64String(privateKey));
                    keyPair = new ECKeyPair(pubKey, privKey);

                    // Make sure the key is valid
                    if(!VerifySecuritySignature(CalculateSecuritySignature()))
                    {
                        keyPair = null;
                    }
                }
            }
        }

        private void GenerateKey(IDataStore dataStore, object context)
        {
            keyPair = Curve.generateKeyPair();

            dataStore.SetString(context, "privateKey", Convert.ToBase64String(keyPair.getPrivateKey().serialize()));
            dataStore.SetString(context, "publicKey", Convert.ToBase64String(keyPair.getPublicKey().serialize()));
        }

        public byte[] SerializePublicKey()
        {
            return keyPair.getPublicKey().serialize();
        }

        public byte[] CalculateSecuritySignature()
        {
            return CalculateSignature(securityString);
        }

        public bool VerifySecuritySignature(byte[] signature)
        {
            return VerifySecuritySignature(keyPair.getPublicKey(), signature);
        }

        public bool VerifySecuritySignature(byte[] publicKey, byte[] signature)
        {
            return VerifySecuritySignature(ConvertPublicKey(publicKey), signature);
        }

        public bool VerifySecuritySignature(ECPublicKey publicKey, byte[] signature)
        {
            return VerifySignature(publicKey, securityString, signature);
        }


        public byte[] CalculateSignature(string data)
        {
            return CalculateSignature(Encoding.UTF8.GetBytes(data));
        }

        public byte[] CalculateSignature(byte[] data)
        {
            return Curve.calculateSignature(keyPair.getPrivateKey(), data);
        }

        public bool VerifySignature(string data, byte[] signature)
        {
            return VerifySignature(StringToBytes(data), signature);
        }

        public bool VerifySignature(byte[] data, byte[] signature)
        {
            return VerifySignature(keyPair.getPublicKey(), data, signature);
        }

        public bool VerifySignature(byte[] publicKey, string data, byte[] signature)
        {
            return VerifySignature(publicKey, StringToBytes(data), signature);
        }

        public bool VerifySignature(byte[] publicKey, byte[] data, byte[] signature)
        {
            return VerifySignature(ConvertPublicKey(publicKey), data, signature);
        }

        public bool VerifySignature(ECPublicKey publicKey, string data, byte[] signature)
        {
            return VerifySignature(publicKey, StringToBytes(data), signature);
        }

        public bool VerifySignature(ECPublicKey publicKey, byte[] data, byte[] signature)
        {
            if (publicKey == null || data == null || signature == null) return false;
            return Curve.verifySignature(publicKey, data, signature);
        }

        private byte[] StringToBytes(string data)
        {
            if (data == null) return null;
            return Encoding.UTF8.GetBytes(data);
        }

        private ECPublicKey ConvertPublicKey(byte[] publicKey)
        {
            if (publicKey == null) return null;
            if (publicKey.Length <= 1 || publicKey[0] != (byte)Curve.DJB_TYPE) return null;
            return new DjbECPublicKey(publicKey.Skip(1).ToArray());
        }
    }
}
