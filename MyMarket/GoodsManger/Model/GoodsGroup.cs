﻿using FreeSql.DataAnnotations;

namespace MyMarket.GoodsManger.Model
{
    public class GoodsGroup
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }

        [Column(IsNullable = false)] public string PDGroup { get; set; }
    }
}