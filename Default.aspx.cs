using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniverusWebTelerik.APIAccess;

namespace UniverusWebTelerik
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var api = new APIHelper();

            var jsonResult = api.CallAPIGet("person/get-all-persons");

            var personDetails = JsonConvert.DeserializeObject<List<PersonDetailsDTO>>(jsonResult.ToString());

            var nameArray = personDetails.Select(x => x.Name).ToArray();

            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart").SetXAxis(new XAxis
            {
                Categories = nameArray
            }).SetSeries(new Series
            {
                Data = new Data(new object[] {
                    29.9,
                    71.5,
                    106.4,
                    129.2,
                    144.0,
                    176.0,
                    135.6,
                    148.5,
                    216.4,
                    194.1,
                    95.6,
                    54.4
                })
            });

           ltrChart.Text = chart.ToHtmlString();
        }
    }
}