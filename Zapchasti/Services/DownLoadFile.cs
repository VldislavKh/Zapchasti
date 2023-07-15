using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using MimeKit;

namespace Presentation.Services
{
    public class DownLoadFile
    {
        public void DownLoad(string email, string password)
        {
            using (var imapClient = new ImapClient())
            {
                imapClient.Connect("imap.gmail.com", 993, true);
                imapClient.Authenticate(email, password);
                imapClient.Inbox.Open(FolderAccess.ReadOnly);

                var uids = imapClient.Inbox.Search(SearchQuery.DeliveredAfter(DateTime.Today.AddDays(-2)));
                var items = imapClient.Inbox.Fetch(uids, MessageSummaryItems.UniqueId | MessageSummaryItems.BodyStructure);

                var recentItemsWithCsv = new List<IMessageSummary>();

                foreach (var item in items)
                {
                    if (item.Attachments.Any(x => x.FileName.EndsWith(".csv")))
                    {
                        recentItemsWithCsv.Add(item);
                    }
                }

                UniqueId uid = recentItemsWithCsv.Last().UniqueId;
                //var t = recentItemsWithCsv.Last();

                MimeMessage message = imapClient.Inbox.GetMessage(uid);
                var attachment = message.Attachments.Single(att => att.ContentDisposition.FileName.EndsWith(".csv"));

                using (var stream = File.Create(@"D:\РАБОТА\Практика\Тестовое\Zapchasti\Price\price.csv"))
                {
                    if (attachment is MimePart)
                    {
                        ((MimePart)attachment).Content.DecodeTo(stream);
                    }
                    stream.Dispose();
                }

                imapClient.Disconnect(true);
            }   
        }
    }
}
