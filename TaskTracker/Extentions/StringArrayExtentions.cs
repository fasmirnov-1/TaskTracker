using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Extentions
{
    public static class StringArrayExtentions
    {
        public static List<FileRecord>ConvertPartsToRecords(this string[] ContentParts)
        {
            List<FileRecord> contentResult = new List<FileRecord>();
            FileRecord record = new FileRecord();

            ContentParts.ToList().ForEach(x => {
                string[] recordParts = x.Split(",");
                recordParts.ToList().ForEach(c => {
                    switch (c.Split(':')[0].Split('"')[1])
                    {
                        case "ID":
                            {
                                record.ID = Convert.ToInt32(c.Split(':')[1]);
                                break;
                            }
                        case "Description":
                            {
                                record.Description = c.Split(';')[0].Split(':')[1].Split('"')[1];
                                break;
                            }
                        case "Status":
                            {
                                record.Status = (_Status)Convert.ToInt32(c.Split(';')[0].Split(':')[1]);
                                break;
                            }
                        case "CreatedAt":
                            {
                                record.CreatedAt = Convert.ToDateTime(c.Split(';')[0].Split(':')[1] + ":" + c.Split(';')[0].Split(':')[2] + ":" + c.Split(';')[0].Split(':')[2]);
                                break;
                            }
                        case "UnpdatedAt":
                            {
                                record.UpdatedAt = Convert.ToDateTime(c.Split(';')[0].Split(':')[1] + ":" + c.Split(';')[0].Split(':')[2] + ":" + c.Split(';')[0].Split(':')[2]);
                                break;
                            }
                    }
                });

                contentResult.Add(record);
                record = new FileRecord();
            });

            return contentResult;
        }
    }
}
