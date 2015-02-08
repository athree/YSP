using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IMserver.Models.SimlDefine;

namespace IMserver.Models
{
    //ʣ���������
    [BsonDiscriminator("siml")]
    public class SIML
    {
        [BsonId]
        public ObjectId DataID { get; set; }  //ʹ��������Ψһ�ı��

        [Display(Name = "�豸��")]
        public string DevID { get; set; }
        public DateTime Time_Stamp { get; set; }  //ʱ���

        //״̬/������Ϣ  ,��Ҫ����
        public StateCtrol SC{ get; set; }

        // ��Ϸ���
        public AnlyInformation AnalyInfo { get; set; }

        public AlarmMsgAll[] AlarmMsg { get; set; }

        //�澯��Ϣ
        public enum AlarmType
        {
            Content = 0,            //��������      
            Absolute,               //���Բ������ʳ���
            Relative                //��Բ������ʳ���
        }

        //����˳��
        public enum PeekOrder
        {
            H2 = 0,
            CO,
            CH4,
            C2H2,
            C2H4,
            C2H6,
            CO2
        }

        public static int GetGasFixParameters(GasFixK[] mk, float area)
        {
            int sel = 0;
            for (int i = 0; i < mk.Length; i++)
            {
                if (area >= mk[i].areaMin && area <= mk[i].areaMax)
                {
                    sel = i;
                    break;
                }
            }
            return sel;
        }
    }
}



