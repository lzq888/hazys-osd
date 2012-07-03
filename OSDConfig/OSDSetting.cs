﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OSDConfig
{
    public enum OSDItem
    {
        Cen,
        Pit,
        Rol,
        BatA,
        BatB,  //(!Not implemented)
        GPSats,
        GPL,
        GPS,

        // panB_REG Byte has:
        Rose,
        Head,
        MavB,
        HDir,
        HDis,
        WDir, //(!Not implemented)
        WDis, //(!Not implemented)
        RSSI, //(!Not implemented)

        // panC_REG Byte has:
        CurA, //(!Not implemented)
        CurB, //(!Not implemented)
        Alt,
        Vel,
        Thr,
        FMod,
        Hor,
        //XXC, //Free
        SYS,
        BatA_ADC,
        BatB_ADC,
        CurA_ADC,
        CurB_ADC,
        RSSI_ADC,
        Alt_R,
        NULL
    }

    public class OSDItemList
    {
        public static string[] Names
        {
            get
            {
                if (Thread.CurrentThread.CurrentUICulture.Name.StartsWith("zh-"))
                    return name_zh;
                else
                    return name_en;
            }
        }

        public static readonly KeyValuePair<OSDItem, OSDItem>[] Conflits = 
        {
            new KeyValuePair<OSDItem, OSDItem>(OSDItem.Cen, OSDItem.Hor)
        };

        public static readonly KeyValuePair<OSDItem, OSDItem>[] Alternates = 
        {
            new KeyValuePair<OSDItem, OSDItem>(OSDItem.BatA, OSDItem.BatA_ADC),
            new KeyValuePair<OSDItem, OSDItem>(OSDItem.BatB, OSDItem.BatB_ADC),
            new KeyValuePair<OSDItem, OSDItem>(OSDItem.RSSI, OSDItem.RSSI_ADC),
            new KeyValuePair<OSDItem, OSDItem>(OSDItem.Alt, OSDItem.Alt_R),
            new KeyValuePair<OSDItem, OSDItem>(OSDItem.CurA, OSDItem.CurA_ADC),
            new KeyValuePair<OSDItem, OSDItem>(OSDItem.CurB, OSDItem.CurB_ADC)
        };

        public static readonly OSDItem[] Avaliable = { 
        OSDItem.Cen,
        OSDItem.Pit,
        OSDItem.Rol,
        OSDItem.BatA,
        OSDItem.BatA_ADC,
        OSDItem.BatB_ADC,
        OSDItem.CurA,
        OSDItem.CurA_ADC,
        OSDItem.CurB_ADC,
        OSDItem.GPSats,
        OSDItem.GPL,
        OSDItem.GPS,

        
        OSDItem.Rose,
        OSDItem.Head,
        OSDItem.MavB,
        OSDItem.HDir,
        OSDItem.HDis,
        OSDItem.RSSI,
        OSDItem.RSSI_ADC,

        OSDItem.Alt,
        OSDItem.Alt_R,
        OSDItem.Vel,
        OSDItem.Thr,
        OSDItem.FMod,
        OSDItem.Hor,

        OSDItem.SYS};


        static readonly string[] name_en = {
            "Center", 
            "Pitch", 
            "Roll", 
            "Voltage A",
            "Voltage B",
            "Visible Sats", 
            "GPS Lock", 
            "GPS Coord", 

            "Heading Rose", 
            "Heading", 
            "Heart Beat", 
            "Home Direction", 
            "Home Distance",
            "Waypoint Direction",
            "Waypoint Distance",
            "RSSI",

            "Current A",
            "Current B",
            "Altitude (Absolute)", 
            "Velocity", 
            "Throttle", 
            "Flight Mode", 
            "Horizon",
            "System Status",

            "Voltage A (AD)",
            "Voltage B (AD)",
            "Current A (AD)",
            "Current B (AD)",
            "RSSI (AD)",
            "Altitude (Relative)"
                                           };

        static readonly string[] name_zh = {
            "中心", 
            "俯仰", 
            "侧倾", 
            "电池A电压",
            "电池B电压",
            "卫星数量", 
            "GPS锁定", 
            "GPS坐标", 

            "航向方位圈", 
            "航向", 
            "心跳", 
            "回家方向", 
            "回家距离", 
            "航点方向",
            "航点距离",
            "信号强度",

            "电池A电流",
            "电池B电流",
            
            "海拔高度", 
            "速度", 
            "油门", 
            "飞行模式", 
            "水平",
            "系统状态",
            "电池A电压 (AD)",
            "电池B电压 (AD)",
            "电池A电流 (AD)",
            "电池B电流 (AD)",
            "信号强度 (AD)",
            "相对高度",
            };
    }

    public class ADSetting
    {
        public byte channel = 0;
        public float k = 1;
        public float b = 0;

        public byte[] ToBytes()
        {
            byte[] buf = new byte[sizeof(byte) + sizeof(float) * 2];
            buf[0] = channel;
            Array.Copy(BitConverter.GetBytes(k), 0, buf, 1, sizeof(float));
            Array.Copy(BitConverter.GetBytes(b), 0, buf, 1 + sizeof(float), sizeof(float));
            return buf;
        }

        public void FromBytes(byte[] data, int offset)
        {
            channel = data[offset];
            k = BitConverter.ToSingle(data, offset + 1);
            b = BitConverter.ToSingle(data, offset + 1 + sizeof(float));
        }

        public const int Size = sizeof(byte) + sizeof(float) * 2;
    }

    public class OSDSetting
    {
        public const byte DataVersion = 5;

        public byte ver;
        public UInt32 enable = _BV(OSDItem.Pit) | _BV(OSDItem.Rol) | _BV(OSDItem.BatA) | _BV(OSDItem.GPSats) | _BV(OSDItem.GPL) | _BV(OSDItem.GPS)
        | _BV(OSDItem.Rose) | _BV(OSDItem.Head) | _BV(OSDItem.MavB) | _BV(OSDItem.HDir) | _BV(OSDItem.HDis)
        | _BV(OSDItem.Alt) | _BV(OSDItem.Vel) | _BV(OSDItem.Thr) | _BV(OSDItem.FMod) | _BV(OSDItem.Hor) | _BV(OSDItem.SYS);

        public byte[,] coord = new byte[24, 2]
        {
            {13, 7}, //  panCenter_y_ADDR
            {22, 9}, //  panPitch_y_ADDR
            {11, 1}, //  panRoll_y_ADDR
            {21, 1}, //  panBatt_A_y_ADDR
            {21, 3}, //  panBatt_B_y_ADDR
            {2, 13}, // panGPSats_y_ADDR
            {5, 13}, // panGPL_y_ADDR
            {2, 14}, // panGPS_y_ADDR
            {16, 14}, // panRose_y_ADDR
            {24, 13}, // panHeading_y_ADDR
            {2, 9}, // panMavBeat_y_ADDR
            {14, 3}, //  panHomeDir_y_ADDR
            {2, 1}, //  panHomeDis_y_ADDR
            {0, 0}, //  panWPDir_y_ADDR
            {0, 0}, //  panWPDis_y_ADDR
            {22, 5}, ////  panRSSI_y_ADDR
            {21, 2}, //  panCur_A_y_ADDR
            {21, 4}, //  panCur_B_y_ADDR
            {2, 2}, //  panAlt_y_ADDR
            {2, 3}, //  panVel_y_ADDR
            {2, 4}, //  panThr_y_ADDR
            {17, 13}, // panFMod_y_ADDR
            {8, 7}, //  panHorizon_y_ADDR
            {11,4}
        };
        public ADSetting[] ad_setting = new ADSetting[] { 
            new ADSetting(), new ADSetting(), new ADSetting(),new ADSetting(), new ADSetting() };

        static UInt32 _BV(OSDItem bi)
        {
            return 1U << (int)bi;
            //_BV(Pit_BIT) | _BV(Rol_BIT) | _BV(BatA_BIT) | _BV(GPSats_BIT) | _BV(GPL_BIT) | _BV(GPS_BIT),
        }

        public bool IsEnabled(OSDItem info)
        {
            return (enable & (1U << (int)info)) != 0;
        }

        public byte[] ToBytes()
        {
            int size = sizeof(byte) + sizeof(uint) + coord.Length + ad_setting.Length * ADSetting.Size;

            byte[] buf = new byte[size];
            buf[0] = ver;
            Array.Copy(BitConverter.GetBytes(enable), 0, buf, sizeof(byte), sizeof(uint));
            Buffer.BlockCopy(coord, 0, buf, sizeof(uint) + sizeof(byte), coord.Length);
            for (int i = 0; i < ad_setting.Length; i++)
                Buffer.BlockCopy(ad_setting[i].ToBytes(), 0,
                    buf, sizeof(byte) + sizeof(uint) + coord.Length + i * ADSetting.Size, ADSetting.Size);


            return buf;
        }

        public bool FromBytes(byte[] data, int offset)
        {
            if (data[0] != DataVersion)
                return false;
            ver = data[0];
            enable = BitConverter.ToUInt32(data, offset + sizeof(byte));
            Buffer.BlockCopy(data, offset + sizeof(byte) + sizeof(uint), coord, 0, coord.Length);
            for (int i = 0; i < ad_setting.Length; i++)
                ad_setting[i].FromBytes(data, offset + sizeof(byte) + sizeof(uint) + coord.Length + i * ADSetting.Size);
            return true;
        }
    }
}
