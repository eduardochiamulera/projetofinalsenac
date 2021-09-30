using AutoMapper;
using System;
using System.Collections.Generic;

namespace Evian.Entities.Adapters
{
    public class GenericAdapter<TFrom, TTo>
    {

        //    public static TTo Adapts(TFrom from, string[] fieldsToIgnore)
        //    {
        //        var conf = new MapperConfiguration(cfg => cfg.CreateMap(typeof(TFrom), typeof(TTo)));


        //        DefaultMapConfig cfg = GetDefaultMapConfig(fieldsToIgnore);

        //        return ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>(cfg).Map(from);
        //    }

        //    public static TTo Adapts(TFrom from, TTo to, string[] fieldsToIgnore)
        //    {
        //        DefaultMapConfig cfg = GetDefaultMapConfig(fieldsToIgnore);

        //        return ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>(cfg).Map(from, to);
        //    }

        //    public static TTo Adapts(TFrom from)
        //    {
        //        string[] fieldsToIgnore = { };
        //        return Adapts(from, fieldsToIgnore);
        //    }

        //    public static TTo Adapts(TFrom from, TTo to)
        //    {
        //        string[] fieldsToIgnore = { };
        //        return Adapts(from, to, fieldsToIgnore);
        //    }

        //    public static List<TTo> AdaptsList(List<TFrom> lst)
        //    {
        //        string[] fieldsToIgnore = { };
        //        return AdaptsList(lst, fieldsToIgnore);
        //    }

        //    public static List<TTo> AdaptsList(List<TFrom> lst, string[] fieldsToIgnore)
        //    {
        //        try
        //        {
        //            List<TTo> ret = new List<TTo>();
        //            foreach (TFrom f in lst)
        //                ret.Add(Adapts(f, fieldsToIgnore));
        //            return ret;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }

        //    private static DefaultMapConfig GetDefaultMapConfig(string[] fieldsToIgnore)
        //    {
        //        DefaultMapConfig defaultMapConfig = new DefaultMapConfig();

        //        if (fieldsToIgnore.Length > 0)
        //            defaultMapConfig.IgnoreMembers<TFrom, TTo>(fieldsToIgnore);

        //        //Forma correta
        //        defaultMapConfig.ConvertUsing<Byte[], string>(v => v != null && v.Length > 0 ? Convert.ToBase64String(v) : string.Empty);
        //        defaultMapConfig.ConvertUsing<string, Byte[]>(v => !string.IsNullOrWhiteSpace(v) ? Convert.FromBase64String(v) : null);

        //        defaultMapConfig.ConvertUsing<string, string>(v => string.IsNullOrEmpty(v) ? String.Empty : v);

        //        defaultMapConfig.ConvertUsing<float?, float>(v => v.HasValue ? (float)v : 0);

        //        return defaultMapConfig;
        //    }
    }
}
