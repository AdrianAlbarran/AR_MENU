using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using TMPro;

public class EmailSender : MonoBehaviour
{
    string eMailDirTo;

    [SerializeField] TMP_InputField eMailTextBox;
    // Start is called before the first frame update 
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendEmail()
    {
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        SmtpServer.Timeout = 10000;
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Port = 587;

        mail.From = new MailAddress("uni67620@gmail.com");
        mail.To.Add(new MailAddress(eMailDirTo));

        mail.Subject = "Descuento Restaurante";
        int random = Random.Range(1000, 10000);
        mail.Body = $"Buenas, \n¡Enseñale este código {random} al camarero de tu restaurante para obtener el descuento en tu comida!\nRecuerda que este código solo funciona para el día de hoy.";


        SmtpServer.Credentials = new System.Net.NetworkCredential("uni67620@gmail.com", "sqjvqbtmaykoeype") as ICredentialsByHost; SmtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };

        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        SmtpServer.Send(mail);
    }

    public void SetEmail()
    {
         eMailDirTo= eMailTextBox.text;
    }
}
