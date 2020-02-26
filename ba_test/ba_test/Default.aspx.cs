using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using CoreLibrary;
using Newtonsoft.Json;

public partial class _Default : System.Web.UI.Page
{
    private const string _url = "https://reqres.in";
    private const string _service = "api/users?";

    private RestService srvc;
    private IList<UserObject> list;

    protected void Page_Load(object sender, EventArgs e)
    {
        save.Enabled = false;
        srvc = new RestService(_url);
    }

    protected void load_Click(object sender, EventArgs e)
    {
        SearchForm frm = new SearchForm();
        if (!string.IsNullOrEmpty(edParam1.Text)) frm.itemsPerPage = Convert.ToInt32(edParam1.Text);
        if (!string.IsNullOrEmpty(edParam2.Text)) frm.PageNum = Convert.ToInt32(edParam2.Text);

        // Очищаем грид
        gv.DataSource = null;
        gv.DataBind();

        var context = new ValidationContext(frm);
        var results = new List<ValidationResult>();
        var formValid = Validator.TryValidateObject(frm, context, results, true);

        if (formValid)
        {
            bool control1_empty = string.IsNullOrEmpty(edParam1.Text);
            bool control2_empty = string.IsNullOrEmpty(edParam2.Text);
            string urlParam = _service;
            urlParam += control1_empty ? "" : "per_page=" + edParam1.Text;
            urlParam += control2_empty ? "" : (urlParam.Length > _service.Length ? "&" : "") + "page=" + edParam2.Text;

            string service_data = srvc.GetData(urlParam);

            DataObject _data = JsonConvert.DeserializeObject<DataObject>(service_data);
            list = _data.data;
            save.Enabled = list.Count > 0;

            gv.DataSource = list;
            gv.DataBind();
        }
        else
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

    protected void save_Click(object sender, EventArgs e)
    {
        IList<UserObject> sel = new List<UserObject>();
        foreach (GridViewRow row in gv.Rows)
        {
            var cb = row.FindControl("selected") as CheckBox;
            if (cb.Checked)
            {
                UserObject o = new UserObject();
                o.id = Convert.ToInt32((row.FindControl("labelID") as Label).Text);
                o.email = (row.FindControl("labelemail") as Label).Text;
                o.first_name = (row.FindControl("labelfirstname") as Label).Text;
                o.last_name = (row.FindControl("labellastname") as Label).Text;
                o.avatar = (row.FindControl("labelavatar") as Label).Text;
                sel.Add(o);
            }
        }
        // Валидируем наличие отмеченных строк
        if (sel.Count == 0)
        {
            MyValidator.IsValid = false;
            MyValidator.ErrorMessage = "Не выбрано ни одной записи";
        }
        else
        {
            string json_data = JsonConvert.SerializeObject(sel);
            byte[] byteArray = Encoding.ASCII.GetBytes(json_data);
            MemoryStream stream = new MemoryStream(byteArray);
            // Дополнительно можно сохранить на диск
            Response.ContentType = "application/force-download";
            Response.AddHeader("Content-Disposition", "attachment; filename=\"report.json\"");
            Response.BufferOutput = false;
            Response.OutputStream.Write(byteArray, 0, byteArray.Length);
            Response.End();
        }
        save.Enabled = true;
    }
}