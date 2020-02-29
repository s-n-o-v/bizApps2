using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using CoreLibrary;
using Newtonsoft.Json;

namespace bizApps
{
    public partial class _Default : System.Web.UI.Page
    {
        private const string _url = "https://reqres.in";
        private const string _service = "api/users?";
        List<UserObject> list;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["list"] != null)
            {
                list = (List<UserObject>)Session["list"];
            }
            else
            {
                list = new List<UserObject>();
            }

            save.Enabled = list.Count > 0;
        }

        protected void load_Click(object sender, EventArgs e)
        {
            if (MyValidator.IsValid)
            {
                mygv.DataSource = null;
                mygv.DataBind();

                RestService srvc = new RestService(_url);

                bool control1_empty = string.IsNullOrEmpty(edParam1.Text);
                bool control2_empty = string.IsNullOrEmpty(edParam2.Text);
                string urlParam = _service;
                urlParam += control1_empty ? "" : "per_page=" + edParam1.Text;
                urlParam += control2_empty ? "" : (urlParam.Length > _service.Length ? "&" : "") + "page=" + edParam2.Text;

                string service_data = srvc.GetData(urlParam);

                DataObject _data = JsonConvert.DeserializeObject<DataObject>(service_data);
                list = _data.data;
                save.Enabled = list.Count > 0;
                Session["list"] = list;

                mygv.DataSource = list;
                mygv.DataBind();
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {
            if (selectedValidator.IsValid)
            {
                string json_data = JsonConvert.SerializeObject(list);
                byte[] byteArray = Encoding.ASCII.GetBytes(json_data);
                MemoryStream stream = new MemoryStream(byteArray);
                // Дополнительно можно сохранить на диск
                Response.ContentType = "application/force-download";
                Response.AddHeader("Content-Disposition", "attachment; filename=\"report.json\"");
                Response.BufferOutput = false;
                Response.OutputStream.Write(byteArray, 0, byteArray.Length);
                Response.End();
            }
        }

        protected void MyValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            SearchForm frm = new SearchForm();
            if (!string.IsNullOrEmpty(edParam1.Text)) frm.itemsPerPage = Convert.ToInt32(edParam1.Text);
            if (!string.IsNullOrEmpty(edParam2.Text)) frm.PageNum = Convert.ToInt32(edParam2.Text);

            var context = new ValidationContext(frm);
            var results = new List<ValidationResult>();
            var formValid = Validator.TryValidateObject(frm, context, results, true);

            if (!formValid)
            {
                string errorList = "";
                foreach (var validationResult in results)
                {
                    errorList += validationResult.ErrorMessage.ToString() + "<br />";
                }
                MyValidator.IsValid = false;
                MyValidator.ErrorMessage = errorList;
            }
        }

        protected void selectedValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            list = new List<UserObject>();

            foreach (GridViewRow row in mygv.Rows)
            {
                var cb = row.FindControl("selected") as CheckBox;
                if (cb.Checked)
                {
                    UserObject o = new UserObject
                    {
                        id = Convert.ToInt32(row.Cells[1].Text),
                        first_name = row.Cells[2].Text,
                        last_name = row.Cells[3].Text,
                        email = row.Cells[4].Text,
                        avatar = row.Cells[5].Text
                    };
                    list.Add(o);
                }
            }
            // Валидируем наличие отмеченных строк
            if (list.Count == 0)
            {
                selectedValidator.IsValid = false;
                selectedValidator.ErrorMessage = "Не выбрано ни одной записи";
            } else
            {
                selectedValidator.IsValid = true;
            }
        }
    }
}