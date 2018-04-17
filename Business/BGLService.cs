using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthEncrypt = AuthenticatedEncryption.AuthenticatedEncryption;

namespace DeafX.Belfour.Business
{
    public class BGLService
    {
        public string AuthKeyBase64 { get; private set; }

        public string CryptKeyBase64 { get; private set; }

        public string BaseUrl { get; private set; }

        private byte[] AuthKey
        {
            get
            {
                return Convert.FromBase64String(AuthKeyBase64);
            }
        }
        private byte[] CryptKey
        {
            get
            {
                return Convert.FromBase64String(CryptKeyBase64);
            }
        }

        public BGLService(string cryptKey, string authKey, string baseUrl)
        {
            AuthKeyBase64 = authKey;
            CryptKeyBase64 = cryptKey;
            BaseUrl = baseUrl;
        }

        public BGLService()
            : this(
                  cryptKey: AuthEncrypt.NewKeyBase64Encoded(),
                  authKey: AuthEncrypt.NewKeyBase64Encoded(),
                  baseUrl: string.Empty
            ) { }

        public string GetBusinessGeneratedLink(object payload)
        {
            var payloadStr = JsonConvert.SerializeObject(payload);

            var encryptedPayload = AuthEncrypt.Encrypt(payloadStr, CryptKey, AuthKey);

            return $"{BaseUrl}?p={encryptedPayload}";
        }
    }
}
