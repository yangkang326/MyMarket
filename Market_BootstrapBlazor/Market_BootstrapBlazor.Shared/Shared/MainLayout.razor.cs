using BootstrapBlazor.Components;

using System.Collections.Generic;


namespace Market_BootstrapBlazor.Shared.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MainLayout
    {
        private bool UseTabSet { get; set; } = true;

        private string Theme { get; set; } = "";

        private bool IsOpen { get; set; }

        private bool IsFixedHeader { get; set; } = true;

        private bool IsFixedFooter { get; set; } = true;

        private bool IsFullSide { get; set; } = false;

        private bool ShowFooter { get; set; } = false;

        private List<MenuItem> Menus { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // TODO: 菜单获取可以通过数据库获取，此处为示例直接拼装的菜单集合
            Menus = GetIconSideMenuItems();
        }
        

        private static List<MenuItem> GetIconSideMenuItems()
        {
            var menus = new List<MenuItem>
            {
                new MenuItem() { Text = "Index", Icon = "fa fa-fw fa-fa", Url = "" },
                new MenuItem() { Text = "杨康的个人介绍", Icon = "fa fa-fw fa-fa", Url = "YANGKANG" },
            };

            return menus;
        }
    }
}
