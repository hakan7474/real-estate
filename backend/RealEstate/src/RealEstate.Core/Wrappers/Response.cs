using System.Net;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace RealEstate.Core
{
    /// <summary>
	/// Servis ya da Business'tan dönen metodları classı dır
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[DataContract(Name = "{0}")]
    public class Response<T>
    {
        #region | Ctor |

        public Response()
        {
            Messages = new Dictionary<string, string>();
        }

        #endregion

        #region  | Properties |

        /// <summary>
        /// Sonuç statüsü
        /// </summary>
        [DataMember]
        public ServiceResponseStatuses Status { get; set; }

        /// <summary>
        /// Dönen data
        /// </summary>
        [DataMember]
        public T Data { get; set; }

        /// <summary>
        /// Serviste client a dönen mesaj listesi
        /// </summary>
        [DataMember]
        public Dictionary<string, string> Messages { get; set; }

        /// <summary>
        /// Serviste client a dönen StatusCode => 200,404,500 vs.
        /// </summary>
        [DataMember]
        [JsonIgnore]
        public int StatusCode { get; set; }

        public int TotalCount { get; set; } = 0;
        #endregion

        #region | Helper Methods |
        public static Response<T> CreateResponse(T data, int totalCount, int statusCode)
        {
            var response = new Response<T> { Status = ServiceResponseStatuses.Success, Data = data, TotalCount = totalCount, StatusCode = statusCode };
            return response;
        }

        public static Response<T> CreateResponse(int statusCode)
        {
            var response = new Response<T> { Status = ServiceResponseStatuses.Success, TotalCount = 0, Data = default(T), StatusCode = statusCode };
            return response;
        }

        public static Response<T> WarningResponse(T data, int totalCount, string messageKey, string message, int statusCode)
        {
            var dictionary = new Dictionary<string, string> { { messageKey, message } };

            var response = new Response<T> { Status = ServiceResponseStatuses.Success, Data = data, TotalCount = totalCount, Messages = dictionary, StatusCode = statusCode };
            return response;
        }

        public static Response<T> WarningResponse(T data, int totalCount, Dictionary<string, string> messages, int statusCode)
        {
            var response = new Response<T> { Status = ServiceResponseStatuses.Success, Data = data, TotalCount = totalCount, Messages = messages, StatusCode = statusCode };
            return response;
        }


        public static Response<T> ErrorResponse(string messageKey, string message, int statusCode)
        {
            var dictionary = new Dictionary<string, string> { { messageKey, message } };

            var response = new Response<T> { Status = ServiceResponseStatuses.Error, Messages = dictionary, TotalCount = 0, StatusCode = statusCode };
            return response;
        }

        public static Response<T> ErrorResponse(Dictionary<string, string> messages, int statusCode)
        {
            var response = new Response<T> { Status = ServiceResponseStatuses.Error, Messages = messages, TotalCount = 0, StatusCode = statusCode };
            return response;
        }


        /// <summary>
        /// Mesaj ekleme
        /// </summary>
        /// <param name="messageKey"></param>
        /// <param name="message"></param>
        public void AddMessage(string messageKey, string message)
        {
            if (this.Messages == null)
            {
                this.Messages = new Dictionary<string, string>();
            }

            var alreadyExists = this.Messages.Any(eachMessage => string.Compare(eachMessage.Key, messageKey, StringComparison.OrdinalIgnoreCase) == 0);

            if (!alreadyExists)
            {
                this.Messages.Add(messageKey, message);
            }
        }

        public bool IsSuccessful()
        {
            this.StatusCode = (int)HttpStatusCode.OK;
            return this.Status == ServiceResponseStatuses.Success;
        }

        #region | Error |

        /// <summary>
        /// Mesaj göndermeden hatalı statüsüne çeker - - Geriye 400 statü koduyla döner
        /// </summary>
        public void Error()
        {
            this.StatusCode = (int)HttpStatusCode.BadRequest;
            this.Status = ServiceResponseStatuses.Error;
        }

        /// <summary>
        /// Mesaj ve hata kodu ile hatalı statü çeker
        /// </summary>
        /// <param name="messageKey"></param>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        public void Error(string messageKey, string message, int statusCode)
        {
            this.Error();
            this.AddMessage(messageKey, message);
        }

        /// <summary>
        /// Mesaj ve hata kodu ile hatalı statü çeker
        /// </summary>
        public void Error(Dictionary<string, string> messages, int statusCode)
        {
            this.Error();
            foreach (var keyValuePair in messages)
            {
                this.AddMessage(keyValuePair.Key, keyValuePair.Value);
            }
        }

        #endregion

        #region | Success |

        /// <summary>
        /// Baraşılı işlem - Geriye 200 statü koduyla döner
        /// </summary>
        public void Success()
        {
            this.StatusCode = (int)HttpStatusCode.OK;
            this.Status = ServiceResponseStatuses.Success;
        }

        /// <summary>
        /// Başarılı işlem data set edilyor
        /// </summary>
        /// <param name="data"></param>
        /// <param name="totalCount"></param>
        /// <param name="statusCode"></param>
        public void Success(T data, int totalCount, int statusCode)
        {
            this.Data = data;
            this.TotalCount = totalCount;
            this.Success();
        }

        /// <summary>
        /// Başarılı işlem data set edilyor ve hata mesajları siliniyor
        /// </summary>
        /// <param name="data"></param>
        /// <param name="clearMessages"></param>
        /// <param name="totalCount"></param>
        /// <param name="statusCode"></param>
        public void Success(T data, int totalCount, int statusCode, bool clearMessages)
        {
            this.Data = data;
            this.TotalCount = totalCount;
            if (clearMessages)
            {
                this.Messages.Clear();
            }
            this.Success();
        }

        #endregion

        #region | Warning |

        /// <summary>
        /// Yapılan işlemler setinde bazılarında hata alıp bazılarında hatasız gerçekleşiyorsa,
        ///  bu servisin işleyişinde hata oluşturmuyorsa bu statüye çekili - Geriye 200 statü koduyla döner
        /// </summary>
        public void Warning()
        {
            this.StatusCode = (int)HttpStatusCode.OK;
            this.Status = ServiceResponseStatuses.Warning;
        }

        /// <summary>
        /// Uyarı mesajları
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageList"></param>
        /// <param name="statusCode"></param>
        public void Warning(T data, Dictionary<string, string> messageList, int statusCode)
        {
            this.Data = data;
            this.Messages = messageList;
            this.Warning();
        }

        /// <summary>
        /// Uyarı mesajları (Liste)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageKey"></param>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        public void Warning(T data, string messageKey, string message, int statusCode)
        {
            this.Data = data;
            this.AddMessage(messageKey, message);
            this.Warning();
        }

        #endregion


        #endregion
    }
}
