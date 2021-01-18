using cms_api.entities;
using cms_api.entities.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace cms_api.services.mail.interfaces
{
    public interface IMailServices
    {
        string getInformMessageBody(MailEntities mailEntities);
        string getResponseMessage(MailEntities mailEntities);

        MailNotification sendNotificationMail(MailEntities mailEntities);
    }
}
