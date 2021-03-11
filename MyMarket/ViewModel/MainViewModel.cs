using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MyMarket.Models;

namespace MyMarket.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private ObservableCollection<CartItem> _CartList;
        private ObservableCollection<GoodsIcon> _ProductsCollection;
        private double _TotalPrice;

        public MainViewModel()
        {
            _ProductsCollection = new ObservableCollection<GoodsIcon>();
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "红苹果",
                PDUnitPrice = 6.5,
                PDUnit = "500g",
                PDSN = "01",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/红苹果.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "青苹果",
                PDUnitPrice = 3.0,
                PDUnit = "500g",
                PDSN = "02",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/青苹果.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "香蕉",
                PDUnitPrice = 4.5,
                PDUnit = "500g",
                PDSN = "03",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/香蕉.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "西瓜",
                PDUnitPrice =7.8,
                PDUnit = "500g",
                PDSN = "04",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/西瓜.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "草莓",
                PDUnitPrice = 15,
                PDUnit = "500g",
                PDSN = "01",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/草莓.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "葡萄",
                PDUnitPrice = 9.9,
                PDUnit = "500g",
                PDSN = "02",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/葡萄.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "黑提",
                PDUnitPrice = 19.9,
                PDUnit = "500g",
                PDSN = "03",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/黑提.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "猕猴桃",
                PDUnitPrice = 6.9,
                PDUnit = "500g",
                PDSN = "04",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/猕猴桃.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "油桃",
                PDUnitPrice = 9.9,
                PDUnit = "500g",
                PDSN = "04",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/油桃.png"
            });
            ProductsCollection.Add(new GoodsIcon
            {
                PDName = "茶Π茉莉花味",
                PDUnitPrice = 9.9,
                PDUnit = "500g",
                PDSN = "04",
                PDIcon = "pack://application:,,,/MyMarket;component/MyImage/香梨.png"
            });
            _CartList = new ObservableCollection<CartItem>();
            AddToCratCommand = new RelayCommand<GoodsIcon>(e =>
            {
                CartList.Add(new CartItem
                {
                    PDName = e.PDName,
                    PDSN = e.PDSN,
                    UnitPrice = e.PDUnitPrice,
                    Count = 1,
                });
                TotalPrice = DOAddTotal(CartList);
            });
            PdContChangedCommand = new RelayCommand(() => { TotalPrice = DOAddTotal(CartList); });
            DeleCartItemCommand = new RelayCommand<CartItem>((e) =>
            {
                CartList.Remove(e);
                TotalPrice = DOAddTotal(CartList);
            });
            PrintDealDetialCommand = new RelayCommand<Grid>((g) =>
            {
                var addheiht = 0;
                if (CartList.Count - 9 > 0)
                {
                    addheiht = (CartList.Count - 9) * 60;
                }
                //PrintDialog pd = new PrintDialog();
                //if (pd.ShowDialog() == true)
                //{
                //    g.Arrange(new Rect(new Size(g.ActualWidth, g.ActualHeight + addheiht)));
                //    pd.PrintVisual(g, "");
                //}
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)g.ActualHeight, (int)g.ActualHeight+addheiht, 96, 96, PixelFormats.Pbgra32);
                rtb.Render(g);
                using (Stream fs = File.Create(@"D:\test.png"))
                {
                    GenerateImage(rtb, ImageFormat.PNG, fs);
                }

            });
        }

        public RelayCommand<CartItem> DeleCartItemCommand { get; set; }
        public ObservableCollection<CartItem> CartList
        {
            get => _CartList;
            set
            {
                _CartList = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<GoodsIcon> AddToCratCommand { get; set; }

        public ObservableCollection<GoodsIcon> ProductsCollection
        {
            get => _ProductsCollection;
            set
            {
                _ProductsCollection = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand PdContChangedCommand { get; set; }

        public double TotalPrice
        {
            get => _TotalPrice;
            set
            {
                _TotalPrice = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<Grid> PrintDealDetialCommand { get; set; }
        private static double DOAddTotal(ObservableCollection<CartItem> listcartitems)
        {
            double temp = 0;
            foreach (var listcartiteml in listcartitems) temp += listcartiteml.PDTotalPrice;
            return temp;
        }
        //定义一个文件类型的枚举
        public enum ImageFormat
        {
            JPG, BMP, PNG, GIF, TIF
        }
        //转为图片并保存
        public void GenerateImage(BitmapSource bitmap, ImageFormat format, Stream destStream)
        {
            BitmapEncoder encoder = null;

            switch (format)
            {
                case ImageFormat.JPG:
                    encoder = new JpegBitmapEncoder();
                    break;
                case ImageFormat.PNG:
                    encoder = new PngBitmapEncoder();
                    break;
                case ImageFormat.BMP:
                    encoder = new BmpBitmapEncoder();
                    break;
                case ImageFormat.GIF:
                    encoder = new GifBitmapEncoder();
                    break;
                case ImageFormat.TIF:
                    encoder = new TiffBitmapEncoder();
                    break;
                default:
                    throw new InvalidOperationException();
            }

            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(destStream);
        }
    }
    public enum ImageFormat
    {
        JPG, BMP, PNG, GIF, TIF
    }
}