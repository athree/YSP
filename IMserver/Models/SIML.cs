
namespace IMserver.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using IMserver.Models.SimlDefine;

    [BsonDiscriminator("siml")]
    public class SIML
    {
        [BsonId]
        public ObjectId DataID { get; set; }  //ʹ��������Ψһ�ı��

        [Display(Name = "�豸��")]

        public string DevID;
        public DateTime Time_Stamp { get; set; }  //ʱ���

        #region ������Ϣ
        //�����΢ˮ����
        public ContentData Content;

        //����ԭʼ����
        public RawData Raw;

        //������������
        public SGasOtherData OtherData;

        #endregion


       ////������Ϣ
       // public Config CFG { get; set; }
       
     


        //״̬/������Ϣ
       
        public StateCtrol SC{ get; set; }

     

      
        // ��Ϸ���

        public AnlyInformation AnalyInfo { get; set; }



        //�澯��Ϣ
        public enum AlarmType
        {
            Content = 0,            //��������      
            Absolute,               //���Բ������ʳ���
            Relative                //��Բ������ʳ���
        }

        public AlarmMsgAll[] AlarmMsg { get; set; }


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



