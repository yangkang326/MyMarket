using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_BootstrapBlazor.Shared.Pages
{
    public partial class YangKang:ComponentBase
    {
        [Inject]
        private string Name { get; set; } = "杨康";
        private string Gender { get; set; } = "男";
        private string Brithday { get; set; } = "1992年3月26";
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
