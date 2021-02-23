using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace GSTAPP.BAL
{
    //public class auth : AuthorizeAttribute
    //{
    //    protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
    //    {
    //        string authString = actionContext.Request.Headers.Authorization.ToString();
    //        try
    //            {
    //            if (authString != null && authString != "userInfo" && authString.StartsWith("userInfo") && authString.IndexOf('`') >= 0)
    //            {
    //                var baseStrings = EnDe.Decrypt((authString.Substring("userInfo ".Length).Trim()).Split(new char[] { '`' })[0], true, authString.Substring(authString.LastIndexOf('`') + 1, 7));//.Split(new char[] { ';' });
    //                var sec = EnDe.Decrypt((authString.Substring("userInfo ".Length).Trim()).Split(new char[] { '`' })[0], true, authString.Substring(authString.LastIndexOf('`') + 1, 7)).Split(new char[] { ';' });
    //                var UserName = sec[0];
    //                var Password = sec[1];
    //                GSTAPP.BAL.UOW _uow = new GSTAPP.BAL.UOW();
    //                var user = _uow.UserMaster.GetSingle(o => o.IsActive == true && o.UserName == UserName && o.Password == Password && o.Tocken.EndsWith(authString.Split('`')[1]));
    //                if (user == null)
    //                {
    //                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
    //                }
    //                UserClass userClass = new UserClass();
    //                userClass.UserName = user.UserName;
    //                userClass.UserId = user.Id;
    //                if(user.UserType=="owner")
    //                {
    //                    userClass.OwnerName = user.UserName;
    //                    userClass.OwnerId = user.Id;
    //                }
    //                else
    //                {
    //                    var owner = _uow.UserMaster.GetSingle(p => p.Id == user.Owner);
    //                    if(owner!=null)
    //                    {
    //                        userClass.OwnerId = owner.Id;
    //                        userClass.OwnerName = owner.UserName;
    //                    }
    //                    else
    //                    {
    //                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
    //            }
    //        }
    //       catch (Exception ex)
    //        {
    //            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));

    //        }

    //    }


    //}

    public static class EnDe
    {
        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            // Get the key from config file

            string key = UserClass.RandomString(7);
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length)+"`"+key;
        }
        public static string Decrypt(string cipherString, bool useHashing,string keyValue)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = keyValue;

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}