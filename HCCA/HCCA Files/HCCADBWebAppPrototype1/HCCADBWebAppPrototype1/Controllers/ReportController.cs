using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text.RegularExpressions;
using System.Diagnostics;
using HCCADBWebAppPrototype1.Models;
using HCCADBWebAppPrototype1.DAL;
using System.Data.Entity;

namespace HCCADBWebAppPrototype1.Controllers
{
    public class ReportController : Controller
    {
        private HCCADatabaseContext db = new HCCADatabaseContext();

        public ActionResult Index()
        {
            //Generate();

            return View();
        }

        public ActionResult Download()
        {
            //Generate();

            //Path
            string path = HttpContext.Server.MapPath("~/App_Data/Downloads/report.docx");

            //return File()
            return File(path, "application/octet-stream", "report.docx");
        }

        public ActionResult Generate(string startDate, string endDate)
        {
            GenerateDocument(startDate, endDate);

            return View();
        }

        public void GenerateDocument(string startDate, string endDate)
        {
            try
            {
                string templateFile = HttpContext.Server.MapPath("~/App_Data/Templates/template.docx");
                string destinationFile = HttpContext.Server.MapPath("~/App_Data/Downloads/report.docx");

                // Create a copy of the template file and open the copy 
                System.IO.File.Copy(templateFile, destinationFile, true);

                string tagHealthCommittees = "HealthCommittees";
                GenerateTable(destinationFile, tagHealthCommittees, startDate, endDate);

                ////open the word document
                //Process.Start(destinationFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GenerateTable(string document, string tag, string startDate, string endDate)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(document, true))
            {
                MainDocumentPart mainPart = wordDoc.MainDocumentPart;

                string fromString = startDate;
                string toString = endDate;
                DateTime fromDate = DateTime.ParseExact(fromString, "dd/MM/yyyy", null);
                DateTime toDate = DateTime.ParseExact(toString, "dd/MM/yyyy", null);
                // date tag
                SdtRun controlTag = mainPart.Document.Body.Descendants<SdtRun>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == "dateTag").SingleOrDefault();
                SdtContentRun contentRun = controlTag.GetFirstChild<SdtContentRun>();
                contentRun.GetFirstChild<Run>().GetFirstChild<Text>().Text = "Date: " + fromDate.ToLongDateString() + " to " + toDate.ToLongDateString();

                // This should return only one content control element: the one with 
                // the specified tag value.
                // If not, "Single()" throws an exception.
                SdtBlock ccWithTable = mainPart.Document.Body.Descendants<SdtBlock>().Where
                    (r => r.SdtProperties.GetFirstChild<Tag>().Val == tag).Single();
                // This should return only one table.
                Table theTable = ccWithTable.Descendants<Table>().Single();

                CurrentStatus cstatus = CurrentStatus.Current;
                EndorsementStatus estatus = EndorsementStatus.Active;

                /***********************Area of health with active committees***********************/
                // display area of health which has active committees
                // in which the active committees have active reps
                // in which the active reps have prep and meeting time
                var areaOfHealth = (from aoh in db.CommitteeAreaOfHealth
                                    from comm in db.Committees
                                    from area in db.CommitteeModel_CommitteeAreaOfHealth
                                    from conh in db.ConsumerRepCommitteeHistory
                                    from conr in db.ConsumerReps
                                    where aoh.CommitteeAreaOfHealthModelID == area.CommitteeAreaOfHealthModelID
                                    where area.CommitteeModelID == comm.CommitteeModelID
                                    where conh.CommitteeModelID == comm.CommitteeModelID
                                    where conr.ConsumerRepModelID == conh.ConsumerRepModelID
                                    where comm.CurrentStatus == cstatus
                                    where conr.EndorsementStatus == estatus
                                    where conh.ReportedDate >= fromDate && conh.ReportedDate <= toDate
                                    where conh.PrepTime > 0 || conh.Meetingtime > 0
                                    select aoh).Distinct();

                int sumTotalCommittees = 0;
                int totalSumTotalReps = 0;
                int totalSumTotalPrepTime = 0;
                int totalSumTotalMeetingTime = 0;
                int totalSumTotalTime = 0;

                foreach (var aoh in areaOfHealth)
                {
                    // copy the table
                    Table tableCopy = (Table)theTable.CloneNode(true);
                    // add the table
                    ccWithTable.AppendChild(tableCopy);

                    // get the first row first element in the table
                    TableRow firstRow = tableCopy.Elements<TableRow>().First();
                    // get the last row in the table
                    TableRow lastRow = tableCopy.Elements<TableRow>().Last();

                    // add value to the first row in the table
                    firstRow.Descendants<TableCell>().ElementAt(0).Append(new Paragraph
                        (new Run(
                            new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "24" }, new Bold()),
                            new Text(aoh.AreaOfHealthName.ToString()))));
                    firstRow.Descendants<TableCell>().ElementAt(1).Append(new Paragraph
                        (new Run(
                            new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }, new Bold()),
                            new Text("Number of Reps"))));
                    firstRow.Descendants<TableCell>().ElementAt(2).Append(new Paragraph
                        (new Run(
                            new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }, new Bold()),
                            new Text("Prep Hours"))));
                    firstRow.Descendants<TableCell>().ElementAt(3).Append(new Paragraph
                        (new Run(
                            new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }, new Bold()),
                            new Text("Meeting Hours"))));
                    firstRow.Descendants<TableCell>().ElementAt(4).Append(new Paragraph
                        (new Run(
                            new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }, new Bold()),
                            new Text("Total Hours"))));

                    // display active committees within the area of health
                    // in which active committees have active reps with prep time or meeting time
                    var committees = (from com in db.Committees
                                      from aohh in db.CommitteeModel_CommitteeAreaOfHealth
                                      from crch in db.ConsumerRepCommitteeHistory
                                      from csr in db.ConsumerReps
                                      where com.CommitteeModelID == aohh.CommitteeModelID
                                      where aohh.CommitteeAreaOfHealthModelID == aoh.CommitteeAreaOfHealthModelID
                                      where crch.CommitteeModelID == com.CommitteeModelID
                                      where crch.ConsumerRepModelID == csr.ConsumerRepModelID
                                      where com.CurrentStatus == cstatus
                                      where csr.EndorsementStatus == estatus
                                      where crch.PrepTime > 0 || crch.Meetingtime > 0
                                      where crch.ReportedDate >= fromDate && crch.ReportedDate <= toDate
                                      select com).Distinct();

                    int totalCommittees = 0;
                    int sumTotalReps = 0;
                    int sumTotalPrepTime = 0;
                    int sumTotalMeetingTime = 0;
                    int sumTotalTime = 0;

                    foreach (var com in committees)
                    {
                        // all consumer reps within the committee
                        var consumerReps = (from cr in db.ConsumerReps
                                            from ch in db.ConsumerRepCommitteeHistory
                                            where ch.CommitteeModelID == com.CommitteeModelID
                                            where ch.ConsumerRepModelID == cr.ConsumerRepModelID
                                            where cr.EndorsementStatus == estatus
                                            where ch.ReportedDate >= fromDate && ch.ReportedDate <= toDate
                                            where ch.PrepTime > 0 || ch.Meetingtime > 0
                                            select cr).Distinct();

                        int totalReps = 0;
                        int totalPrepTime = 0;
                        int totalMeetingTime = 0;
                        int totalTime = 0;

                        foreach (var cr in consumerReps)
                        {
                            // newest reported date of the consumer rep
                            var maxReportedDate = (from ch in db.ConsumerRepCommitteeHistory
                                                   where cr.ConsumerRepModelID == ch.ConsumerRepModelID
                                                   where ch.CommitteeModelID == com.CommitteeModelID
                                                   where ch.ReportedDate >= fromDate && ch.ReportedDate <= toDate
                                                   where cr.EndorsementStatus == estatus
                                                   select ch.ReportedDate).Max();

                            // newest consumer rep history of the consumer rep
                            var consumerRepHisotry = (from ch in db.ConsumerRepCommitteeHistory
                                                      where cr.ConsumerRepModelID == ch.ConsumerRepModelID
                                                      where ch.CommitteeModelID == com.CommitteeModelID
                                                      where ch.ReportedDate >= fromDate && ch.ReportedDate <= toDate
                                                      where cr.EndorsementStatus == estatus
                                                      where ch.ReportedDate == maxReportedDate
                                                      select ch).Distinct();

                            // total
                            totalReps++;

                            foreach (var ch in consumerRepHisotry)
                            {
                                totalPrepTime += ch.PrepTime;
                                totalMeetingTime += ch.Meetingtime;
                                totalTime = totalMeetingTime + totalPrepTime;
                            }
                        }

                        // row for each committee
                        TableRow rowCopy = (TableRow)lastRow.CloneNode(true);
                        rowCopy.Descendants<TableCell>().ElementAt(0).Append(new Paragraph
                            (new Run(
                                new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }),
                                new Text(com.CommitteeName.ToString()))));
                        rowCopy.Descendants<TableCell>().ElementAt(1).Append(new Paragraph
                            (new Run(
                                new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }),
                                new Text(totalReps.ToString()))));
                        rowCopy.Descendants<TableCell>().ElementAt(2).Append(new Paragraph
                            (new Run(
                                new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }),
                                new Text(totalPrepTime.ToString()))));
                        rowCopy.Descendants<TableCell>().ElementAt(3).Append(new Paragraph
                            (new Run(
                                new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }),
                                new Text(totalMeetingTime.ToString()))));
                        rowCopy.Descendants<TableCell>().ElementAt(4).Append(new Paragraph
                            (new Run(
                                new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }),
                                new Text(totalTime.ToString()))));
                        tableCopy.AppendChild(rowCopy);

                        // sum of total
                        totalCommittees++;
                        sumTotalReps += totalReps;
                        sumTotalPrepTime += totalPrepTime;
                        sumTotalMeetingTime += totalMeetingTime;
                        sumTotalTime += totalTime;
                    }
                    // remove the empty placeholder row from the table.
                    tableCopy.RemoveChild(lastRow);

                    // add a final row to the table
                    tableCopy.AppendChild(lastRow);
                    lastRow.Descendants<TableCell>().ElementAt(0).Append(new Paragraph
                            (new Run(
                                new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }, new Bold()),
                                new Text("Committees active in the period: " + totalCommittees.ToString()))));
                    lastRow.Descendants<TableCell>().ElementAt(1).Append(new Paragraph
                            (new Run(
                                new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }, new Bold()),
                                new Text(sumTotalReps.ToString()))));
                    lastRow.Descendants<TableCell>().ElementAt(2).Append(new Paragraph
                            (new Run(
                                new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }, new Bold()),
                                new Text(sumTotalPrepTime.ToString()))));
                    lastRow.Descendants<TableCell>().ElementAt(3).Append(new Paragraph
                            (new Run(
                                new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }, new Bold()),
                                new Text(sumTotalMeetingTime.ToString()))));
                    lastRow.Descendants<TableCell>().ElementAt(4).Append(new Paragraph
                            (new Run(
                                new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }, new Bold()),
                                new Text(sumTotalTime.ToString()))));

                    // total sum of total
                    sumTotalCommittees += totalCommittees;
                    totalSumTotalReps += sumTotalReps;
                    totalSumTotalPrepTime += sumTotalPrepTime;
                    totalSumTotalMeetingTime += sumTotalMeetingTime;
                    totalSumTotalTime += sumTotalTime;
                }
                //remove the template table
                theTable.Remove();

                //***********************Other active committees***********************/
                //get last row of active committees
                TableRow lastRowActive = theTable.Elements<TableRow>().Last();
                //get first row of active committees
                TableRow firstRowActive = theTable.Elements<TableRow>().First();

                //first row values
                firstRowActive.Descendants<TableCell>().ElementAt(0).Append(new Paragraph
                    (new Run(
                        new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "32" }, new Bold(), new Italic()),
                        new Text("Active Committees that Have Not Met in This Period".ToString()))));
                firstRowActive.Descendants<TableCell>().ElementAt(1).Append(new Paragraph
                        (new Run(
                            new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }, new Bold()),
                            new Text("Number of Reps".ToString()))));

                var activeCommittees = (from aCom in db.Committees
                                        from aComh in db.ConsumerRepCommitteeHistory
                                        where aComh.CommitteeModelID == aCom.CommitteeModelID
                                        where aComh.PrepTime == 0 && aComh.Meetingtime == 0
                                        where aComh.ReportedDate >= fromDate && aComh.ReportedDate <= toDate
                                        where aCom.CurrentStatus == cstatus
                                        select aCom).Distinct();

                int totalcommittees = 0;
                int totalsumrep = 0;

                foreach (var aCom in activeCommittees)
                {
                    var activeReps = (from aRep in db.ConsumerReps
                                      from aComh in db.ConsumerRepCommitteeHistory
                                      where aRep.ConsumerRepModelID == aComh.ConsumerRepModelID
                                      where aComh.CommitteeModelID == aCom.CommitteeModelID
                                      where aComh.ReportedDate >= fromDate && aComh.ReportedDate <= toDate
                                      where aRep.EndorsementStatus == estatus
                                      select aRep).Distinct();

                    int totalprep = 0;
                    int totalmeet = 0;
                    int totalrep = 0;

                    foreach (var aRep in activeReps)
                    {
                        var maxDate = (from aComh in db.ConsumerRepCommitteeHistory
                                       where aComh.CommitteeModelID == aCom.CommitteeModelID
                                       where aComh.ConsumerRepModelID == aRep.ConsumerRepModelID
                                       where aComh.ReportedDate >= fromDate && aComh.ReportedDate <= toDate
                                       select aComh.ReportedDate).Max();

                        var activeComh = from aComh in db.ConsumerRepCommitteeHistory
                                         where aComh.CommitteeModelID == aCom.CommitteeModelID
                                         where aComh.ConsumerRepModelID == aRep.ConsumerRepModelID
                                         where aComh.ReportedDate >= fromDate && aComh.ReportedDate <= toDate
                                         where aComh.ReportedDate == maxDate
                                         select aComh;

                        foreach (var aComh in activeComh)
                        {
                            totalprep += aComh.PrepTime;
                            totalmeet += aComh.Meetingtime;
                        }

                        totalrep++;
                    }

                    if (totalprep == 0 && totalmeet == 0)
                    {
                        TableRow rowCopyActive = (TableRow)lastRowActive.CloneNode(true);
                        rowCopyActive.Descendants<TableCell>().ElementAt(0).Append(new Paragraph
                            (new Run(
                                new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }),
                                new Text(aCom.CommitteeName.ToString()))));
                        rowCopyActive.Descendants<TableCell>().ElementAt(1).Append(new Paragraph
                            (new Run(
                                new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }),
                                new Text(totalrep.ToString()))));
                        // add row
                        theTable.AppendChild(rowCopyActive);

                        // total active committees and reps that is doesn't have a prep time
                        totalcommittees++;
                        totalsumrep += totalrep;
                    }
                }
                // remove empty row
                theTable.RemoveChild(lastRowActive);

                /***********************Total number***********************/
                sumTotalCommittees += totalcommittees;
                totalSumTotalReps += totalsumrep;

                // three more rows
                for (int i = 0; i < 3; i++)
                {
                    TableRow rowFinalCopy = (TableRow)lastRowActive.CloneNode(true);
                    theTable.AppendChild(rowFinalCopy);
                }
                // Get the last row in the last two rows
                TableRow theLastRow = theTable.Elements<TableRow>().Last();
                // Get the second last row
                TableRow theSecondLastRow = theTable.Elements<TableRow>().Reverse().Skip(1).First();
                // Get the third last row
                TableRow theThirdLastRow = theTable.Elements<TableRow>().Reverse().Skip(2).First();

                // edit the final three rows
                theThirdLastRow.Descendants<TableCell>().ElementAt(0).Append(new Paragraph
                    (new Run(
                        new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }, new Bold()),
                        new Text("Committees Active in the Period in this Grouping: " + totalcommittees.ToString()))));
                theThirdLastRow.Descendants<TableCell>().ElementAt(1).Append(new Paragraph
                        (new Run(
                            new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "20" }, new Bold()),
                            new Text(totalsumrep.ToString()))));

                theSecondLastRow.Descendants<TableCell>().ElementAt(1).Append(new Paragraph
                        (new Run(
                            new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "32" }, new Bold(), new Italic()),
                            new Text("Total Reps".ToString()))));
                theSecondLastRow.Descendants<TableCell>().ElementAt(2).Append(new Paragraph
                        (new Run(
                            new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "32" }, new Bold(), new Italic()),
                            new Text("Total Prep Hours".ToString()))));
                theSecondLastRow.Descendants<TableCell>().ElementAt(3).Append(new Paragraph
                        (new Run(
                            new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "32" }, new Bold(), new Italic()),
                            new Text("Total Meeting Hours"))));
                theSecondLastRow.Descendants<TableCell>().ElementAt(4).Append(new Paragraph
                        (new Run(
                            new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "32" }, new Bold(), new Italic()),
                            new Text("Total Hours"))));

                theLastRow.Descendants<TableCell>().ElementAt(0).Append(new Paragraph
                        (new Run(
                            new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "32" }, new Bold(), new Italic()),
                            new Text("Total Committees Active in this Period: " + sumTotalCommittees.ToString()))));
                theLastRow.Descendants<TableCell>().ElementAt(1).Append(new Paragraph
                        (new Run(
                            new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "32" }, new Bold(), new Italic()),
                            new Text(totalSumTotalReps.ToString()))));
                theLastRow.Descendants<TableCell>().ElementAt(2).Append(new Paragraph
                        (new Run(
                            new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "32" }, new Bold(), new Italic()),
                            new Text(totalSumTotalPrepTime.ToString()))));
                theLastRow.Descendants<TableCell>().ElementAt(3).Append(new Paragraph
                        (new Run(
                            new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "32" }, new Bold(), new Italic()),
                            new Text(totalSumTotalMeetingTime.ToString()))));
                theLastRow.Descendants<TableCell>().ElementAt(4).Append(new Paragraph
                        (new Run(
                            new RunFonts { Ascii = "Arial" }, new RunProperties(new FontSize { Val = "32" }, new Bold(), new Italic()),
                            new Text(totalSumTotalTime.ToString()))));

                // add the template table
                ccWithTable.AppendChild(theTable);

                // Save the changes to the table back into the document.
                mainPart.Document.Save();
            }
        }
    }
}