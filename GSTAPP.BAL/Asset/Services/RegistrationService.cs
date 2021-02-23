using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class RegistrationService : Service
    {
        public ReturnModel<RegistrationMaster> Register(RegistrationMaster model)
        {
            var rModel = new ReturnModel<RegistrationMaster>();
            if (uow.RegistrationMaster.GetSingle(d => d.Email == model.Email || d.PhoneNo == model.PhoneNo) != null)
            {
                rModel.code = "1";
                rModel.error = "User details already exist!";
                return rModel;
            }
            var inData = new DAL.RegistrationMaster()
            {
                Email = model.Email,
                EmailVerification = false,
                PhoneNo = model.PhoneNo,
                PhoneNoVerification = false,
                Password = model.Password,
                EmailVerificationCode = RandomNumbers.RandomString(7),
                PhoneNoVerificationCode = RandomNumbers.RandomString(7),
                ValidAt = DateTime.Now.AddMinutes(30)
            };
            uow.RegistrationMaster.Add(inData);
            sendEmail(inData,model.URLData);
            rModel.code = "0";
            return rModel;
        }

        public ReturnModel<RegistrationMaster> GetSingleByEmailorPhoneNo(RegistrationMaster model)
        {
            var data = uow.RegistrationMaster.GetSingle(o => o.Email == model.Email || o.PhoneNo == model.PhoneNo);
            if (data != null)
            {
                return new ReturnModel<RegistrationMaster>()
                {
                    code = "0",
                    datam = new RegistrationMaster()
                    {
                        Email = data.Email,
                        PhoneNo = data.PhoneNo
                    }
                };
            }
            return new ReturnModel<RegistrationMaster>()
            {
                code = "1",
                error = "No Registration found!"
            };
        }

        public ReturnModel<RegistrationMaster> EmailVerification(RegistrationMaster model)
        {
            var data = uow.RegistrationMaster.GetSingle(o => o.Email == model.Email);
            if (data != null)
            {
                if (data.EmailVerification == true)
                {
                    return new ReturnModel<RegistrationMaster>()
                    {
                        code = "0",
                        datam = model
                    };
                }
                if (model.EmailVerificationCode != null && model.EmailVerificationCode.ToString().Length > 0 && data.EmailVerificationCode.ToString() == model.EmailVerificationCode.ToString() && data.ValidAt >= DateTime.Now)
                {
                    data.EmailVerification = true;
                }
                else if (data.ValidAt < DateTime.Now)
                {
                    return new ReturnModel<RegistrationMaster>()
                    {
                        code = "2",
                        error = "Verification code was expired!"
                    };
                }
                else
                {
                    return new ReturnModel<RegistrationMaster>()
                    {
                        code = "1",
                        error = "No data found!"
                    };
                }
            }
            else
            {
                return new ReturnModel<RegistrationMaster>()
                {
                    code = "1",
                    error = "No data found!"
                };
            }
            uow.RegistrationMaster.Update(data);
            createUser(data);
            model.EmailVerification = true;
            return new ReturnModel<RegistrationMaster>()
            {
                code = "0",
                datam = model
            };
        }

        public ReturnModel<RegistrationMaster> PhoneVerification(RegistrationMaster model)
        {
            var data = uow.RegistrationMaster.GetSingle(o => o.PhoneNo == model.PhoneNo);
            if (data.Id.ToString().Length > 0)
            {
                if (model.PhoneNoVerificationCode != null && model.PhoneNoVerificationCode.ToString().Length > 0 && data.PhoneNoVerificationCode.ToString() == model.PhoneNoVerificationCode.ToString() && data.ValidAt >= DateTime.Now)
                {
                    data.PhoneNoVerification = true;
                }
                else if (data.ValidAt < DateTime.Now)
                {
                    return new ReturnModel<RegistrationMaster>()
                    {
                        code = "2",
                        error = "Verification code was expired!"
                    };
                }
                else
                {
                    return new ReturnModel<RegistrationMaster>()
                    {
                        code = "1",
                        error = "No data found!"
                    };
                }
            }
            else
            {
                return new ReturnModel<RegistrationMaster>()
                {
                    code = "1",
                    error = "No data found!"
                };
            }
            uow.RegistrationMaster.Update(data);
            model.PhoneNoVerification = true;
            return new ReturnModel<RegistrationMaster>()
            {
                code = "0",
                datam = model
            };
        }

        public ReturnModel<RegistrationMaster> EmailandPhoneIsVerifid(RegistrationMaster model)
        {
            var s = uow.RegistrationMaster.GetSingle(o => o.Email == model.Email && o.PhoneNo == model.PhoneNo);
            if (s == null)
            {
                return new ReturnModel<RegistrationMaster>()
                {
                    code = "1",
                    error = "No data found!"
                };
            }
            createUser(s);
            return new ReturnModel<RegistrationMaster>()
            {
                code = "0",
                datam = new RegistrationMaster()
                {
                    EmailVerification = s.EmailVerification,
                    PhoneNoVerification = s.PhoneNoVerification
                }
            };
        }

        
        public ReturnModel<RegistrationMaster> ResendData(RegistrationMaster model)
        {
            var s = uow.RegistrationMaster.GetSingle(o => o.Email == model.Email || o.PhoneNo == model.PhoneNo);
            if (s != null)
            {
                if (s.ValidAt < DateTime.Now)
                {
                    s.EmailVerification = false;
                    s.PhoneNoVerification = false;
                    s.EmailVerificationCode = UserClass.RandomString(7);
                    s.PhoneNoVerificationCode = UserClass.RandomString(7);
                    s.ValidAt = DateTime.Now.AddMinutes(30);
                    uow.RegistrationMaster.Update(s);
                    sendEmail(s,model.URLData);
                }
                else if (s.ValidAt >= DateTime.Now)
                {
                    if (s.EmailVerification == false)
                    {
                        sendEmail(s, model.URLData);
                    }
                    if (s.PhoneNoVerification == false)
                    {
                        //phone verification
                    }
                }
            }
            return new ReturnModel<RegistrationMaster>()
            {
                code = "0",
                datam = new RegistrationMaster()
                {
                    Email = s.Email,
                    PhoneNo = s.PhoneNo,
                    EmailVerification = s.EmailVerification,
                    PhoneNoVerification = s.PhoneNoVerification
                }
            };
        }


        private void createUser(DAL.RegistrationMaster model)
        {
            if (model.EmailVerification == true /*&& model.PhoneNoVerification == true*/ && uow.UserMaster.GetAll(o=>o.UserName == model.Email).Count() == 0)
            {
                uow.UserMaster.Add(new DAL.UserMaster() { Id = 0, IsActive = true, UserName = model.Email, Password = model.Password, LastLogin = DateTime.Now, UserType = "owner", Tocken = "", CreatedBy = 0, CreatedOn = DateTime.Now });
            }
        }
        private void sendEmail(DAL.RegistrationMaster s,string url)
        {

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new System.Net.Mail.MailAddress("skyrandsoftwares@gmail.com");

            mail.To.Add(s.Email);
            mail.Subject = "Email Verification";

            mail.Body = "\n Click to verify your account \n"+ url+s.Email+"/"+s.EmailVerificationCode + "  Link valid for 30 minutes only (If mail in spam move to inbox)";
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);


            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential(mail.From.Address, "test@1234");

            try
            {
                smtp.Send(mail);
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.ToString());
            }
        }
    }
}
