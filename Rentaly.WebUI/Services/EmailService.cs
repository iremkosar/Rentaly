using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Rentaly.WebUI.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendReservationConfirmationAsync(string toEmail, string toName, int reservationId, string discountCode, DateTime pickUpDateTime, DateTime returnDateTime)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Rentaly", _settings.Email));
            message.To.Add(new MailboxAddress(toName, toEmail));
            message.Subject = "Rezervasyonunuz Onaylandı ✓";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = GetEmailTemplate(toName, reservationId, discountCode, pickUpDateTime, returnDateTime);
            message.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_settings.Email, _settings.Password);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }

        private string GetEmailTemplate(string name, int reservationId, string discountCode, DateTime pickUpDateTime, DateTime returnDateTime)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        * {{ margin:0; padding:0; box-sizing:border-box; }}
        body {{ font-family: 'Segoe UI', Arial, sans-serif; background:#080808; }}
        .wrapper {{ max-width:520px; margin:0 auto; padding:30px 16px; }}
        .card {{ background:#111; border-radius:18px; overflow:hidden; border:1px solid #222; }}

        .topbar {{ height:2px; background:linear-gradient(90deg, #3a3a3a, #c9a84c, #3a3a3a); }}

        .hdr {{ padding:38px 36px 28px; text-align:center; background:#0c0c0c; border-bottom:1px solid #1a1a1a; }}
        .logo-txt {{ font-size:26px; font-weight:300; letter-spacing:12px; color:#fff; }}
        .logo-txt span {{ color:#c9a84c; }}
        .gold-line {{ width:36px; height:1px; background:linear-gradient(90deg, transparent, #c9a84c, transparent); margin:12px auto 10px; }}
        .tagline {{ color:#555; font-size:9px; letter-spacing:4px; text-transform:uppercase; }}

        .body {{ padding:38px 36px; background:#111; }}
        .greet {{ font-size:21px; color:#e8e8e8; font-weight:300; margin-bottom:8px; }}
        .greet strong {{ color:#c9a84c; font-weight:500; }}
        .sub {{ color:#888; font-size:13px; line-height:1.9; margin-bottom:28px; }}

        .badge {{ display:inline-flex; align-items:center; gap:8px; background:rgba(201,168,76,0.08); border:1px solid rgba(201,168,76,0.22); color:#c9a84c; padding:8px 22px; border-radius:50px; font-size:10px; letter-spacing:2px; text-transform:uppercase; margin-bottom:28px; }}

        .dates {{ display:flex; gap:10px; margin-bottom:28px; align-items:center; }}
        .dbox {{ flex:1; background:#0d0d0d; border:1px solid #1e1e1e; border-radius:10px; padding:18px; text-align:center; }}
        .dlabel {{ font-size:9px; color:#555; letter-spacing:3px; text-transform:uppercase; margin-bottom:9px; }}
        .ddate {{ font-size:14px; color:#ccc; font-weight:400; }}
        .dtime {{ font-size:12px; color:#888; margin-top:5px; }}
        .darrow {{ color:#333; font-size:16px; flex-shrink:0; }}

        .disc {{ background:#0d0d0d; border:1px solid #1e1e1e; border-radius:14px; padding:32px 28px; text-align:center; margin-bottom:24px; position:relative; }}
        .disc::before {{ content:''; position:absolute; top:0; left:15%; right:15%; height:1px; background:linear-gradient(90deg, transparent, rgba(201,168,76,0.35), transparent); }}
        .disc-label {{ font-size:9px; color:#555; letter-spacing:4px; text-transform:uppercase; margin-bottom:20px; }}
        .disc-code {{ font-size:30px; font-weight:700; color:#e2e2e2; letter-spacing:8px; font-family:monospace; margin-bottom:14px; }}
        .disc-badge {{ display:inline-block; background:rgba(201,168,76,0.1); border:1px solid rgba(201,168,76,0.2); color:#c9a84c; font-size:10px; letter-spacing:2px; padding:5px 16px; border-radius:50px; margin-bottom:14px; }}
        .disc-desc {{ font-size:12px; color:#bbb; line-height:1.9; }}

        .ref {{ border-top:1px solid #1a1a1a; padding-top:18px; }}
        .ref-row {{ display:flex; justify-content:space-between; align-items:center; padding:8px 0; }}
        .ref-lbl {{ font-size:11px; color:#555; letter-spacing:1px; text-transform:uppercase; }}
        .ref-val {{ font-size:13px; color:#aaa; font-family:monospace; }}

        .ftr {{ padding:22px 36px; text-align:center; border-top:1px solid #1a1a1a; background:#0c0c0c; }}
        .ftr-brand {{ color:#444; font-size:10px; letter-spacing:5px; display:block; margin-bottom:8px; }}
        .ftr-txt {{ font-size:11px; color:#444; line-height:2; }}
    </style>
</head>
<body>
    <div class='wrapper'>
        <div class='card'>
            <div class='topbar'></div>

            <div class='hdr'>
                <div class='logo-txt'>RENT<span>A</span>LY</div>
                <div class='gold-line'></div>
                <div class='tagline'>Premium Car Rental</div>
            </div>

            <div class='body'>
                <div class='greet'>Merhaba, <strong>{name}</strong></div>
                <p class='sub'>Rezervasyonunuz onaylanmıştır. Aşağıda seyahat detaylarınızı ve size özel indirim kodunuzu bulabilirsiniz.</p>

                <div class='badge'>✓ &nbsp; Rezervasyon Onaylandı</div>

                <div class='dates'>
                    <div class='dbox'>
                        <div class='dlabel'>Alış Tarihi</div>
                        <div class='ddate'>{pickUpDateTime.ToString("dd MMM yyyy")}</div>
                        <div class='dtime'>{pickUpDateTime.ToString("HH:mm")}</div>
                    </div>
                    <div class='darrow'>→</div>
                    <div class='dbox'>
                        <div class='dlabel'>Dönüş Tarihi</div>
                        <div class='ddate'>{returnDateTime.ToString("dd MMM yyyy")}</div>
                        <div class='dtime'>{returnDateTime.ToString("HH:mm")}</div>
                    </div>
                </div>

                <div class='disc'>
                    <div class='disc-label'>✦ &nbsp; size özel indirim kodu &nbsp; ✦</div>
                    <div class='disc-code'>{discountCode}</div>
                    <div class='disc-badge'>%15 ÖZEL İNDİRİM</div>
                    <div class='disc-desc'>Bir sonraki rezervasyonunuzda bu kodu kullanabilirsiniz.</div>
                </div>
               
            </div>

            <div class='ftr'>
                <span class='ftr-brand'>RENTALY</span>
                <p class='ftr-txt'>
                    Bu e-posta otomatik olarak gönderilmiştir.<br>
                    Sorularınız için destek hattımızla iletişime geçebilirsiniz.
                </p>
            </div>
        </div>
    </div>
</body>
</html>";
        }
    }
}