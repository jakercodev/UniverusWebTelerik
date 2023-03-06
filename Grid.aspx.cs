using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using UniverusWebTelerik.APIAccess;
using UniverusWebTelerik.Helper;

namespace UniverusWebTelerik
{
    public partial class Grid : System.Web.UI.Page
    {

        public Grid()
        {

        }


        public DataTable Persons
        {
            get
            {
                return GetPersonList();
            }
        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = this.Persons;
        }

        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
        }

        protected void RadGrid1_InsertCommand(object sender, GridCommandEventArgs e)
        {
            Hashtable table = new Hashtable();
            (e.Item as GridEditableItem).ExtractValues(table);

            PersonDTO person = new PersonDTO
            {
                Name = table["Name"].ToString(),
                Age = Convert.ToInt32(table["Age"]),
                PersonTypeId = Convert.ToInt32(table["PersonType"])
            };

            var api = new APIHelper();

            var jsonResult = api.CallAPIPost("person/create-person", person);
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //this.Sellers.Rows.Remove(this.Sellers.Rows.Find((e.Item as GridEditableItem).GetDataKeyValue("ID")));
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            GridHeaderItem headerItem = e.Item as GridHeaderItem;
            if (headerItem != null)
            {
                headerItem["EditColumn"].Text = string.Empty;
                headerItem["DeleteColumn"].Text = string.Empty;
            }
        }

        public DataTable GetPersonList()
        {
            var api = new APIHelper();

            DataTable dt = new DataTable();

            var jsonResult = api.CallAPIGet("person/get-all-persons");

            var personDetails = JsonConvert.DeserializeObject<List<PersonDetailsDTO>>(jsonResult.ToString());

            dt = HelperConverter.ToDataTable(personDetails);

            return dt;
        }

 
    }
}
