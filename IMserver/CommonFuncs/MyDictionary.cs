using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMserver.CommonFuncs
{
    public class MyDictionary
    {
        public static Dictionary<ushort , ushort> unittypedict = new Dictionary<ushort, ushort>();
        public static Dictionary<ushort , ushort> unitlendict = new Dictionary<ushort, ushort>();

        //public static Dictionary<byte , string> ID_IP = new Dictionary<byte, string>();
        //public static Dictionary<byte, int> ID_PORT = new Dictionary<byte, int>()；

        public MyDictionary()
        {
            #region   操作单元数据类型字典
            //真空脱气装置
            unittypedict.Add(0x0003, 1);	//循环时间查询/设置
            unittypedict.Add(0x0004, 0);	//抽空次数查询/设置
            unittypedict.Add(0x0005, 0);	//清洗次数查询/设置
            unittypedict.Add(0x0006, 0);	//脱气次数查询/设置
            unittypedict.Add(0x0007, 0);	//置换次数查询/设置
            unittypedict.Add(0x0008, 1);	//脱气机预计脱气完成时间
            unittypedict.Add(0x000A, 2);	//（脱气机）真空度压力检测值
            unittypedict.Add(0x000B, 2);	//（脱气机）气泵气压检测值
            unittypedict.Add(0x000C, 2);	//（脱气机）油压检测值
            unittypedict.Add(0x000D, 0);	//（脱气机）油杯液位状态
            unittypedict.Add(0x000E, 0);	//（脱气机）气杯液位状态
            unittypedict.Add(0x000F, 0);	//（脱气机）气缸进到位
            unittypedict.Add(0x0010, 0);	//（脱气机）气缸退到位
            unittypedict.Add(0x0011, 0);	//（脱气机）油缸进到位
            unittypedict.Add(0x0012, 0);	//（脱气机）油缸退到位
            unittypedict.Add(0x0014, 0);	//（脱气机）油泵
            unittypedict.Add(0x0015, 2);	//（脱气机）油泵转速控制输出
            unittypedict.Add(0x0016, 0);	//（脱气机）进出油阀
            unittypedict.Add(0x0017, 0);	//（脱气机）YV10阀
            unittypedict.Add(0x0018, 0);	//（脱气机）YV11阀
            unittypedict.Add(0x0019, 0);	//（脱气机）YV12阀
            unittypedict.Add(0x001A, 0);	//（脱气机）YV14阀
            unittypedict.Add(0x001B, 0);	//（脱气机）YV15阀
            unittypedict.Add(0x001C, 0);	//（脱气机）气缸YV4阀
            unittypedict.Add(0x001D, 0);	//（脱气机）气缸YV5阀
            unittypedict.Add(0x001E, 0);	//（脱气机）气缸YV6阀
            unittypedict.Add(0x001F, 0);	//（脱气机）气缸YV7阀
            unittypedict.Add(0x0020, 0);	//（脱气机）气泵
            //膜脱气装置
            unittypedict.Add( 0x0023	, 1);	//（膜脱气）油泵连续工作时间
            unittypedict.Add( 0x0024	, 1);	//（膜脱气）排气阀清洗时间
            unittypedict.Add( 0x0025	, 1);	//（膜脱气）气泵清洗时间
            unittypedict.Add( 0x0026	, 1);	//（膜脱气）排气阀连续工作时间
            unittypedict.Add( 0x0027	, 1);	//（膜脱气）气泵连续工作时间
            unittypedict.Add( 0x0028	, 1);	//（膜脱气）排气阀间隔停止时间
            unittypedict.Add( 0x0029	, 1);	//（膜脱气）气泵间隔停止时间
            //顶空脱气装置
            unittypedict.Add( 0x002C	, 0);	//（手动）搅拌开关（立即开始）
            unittypedict.Add( 0x002E	, 0);	//液位A
            unittypedict.Add( 0x002F	, 0);	//液位B
            unittypedict.Add( 0x0030	, 2);	//（顶空方式）搅拌缸检测温度
            unittypedict.Add( 0x0032	, 1);	//（顶空方式）搅拌开始时刻
            unittypedict.Add( 0x0033	, 1);	//（顶空方式）搅拌工作时间
            unittypedict.Add( 0x0034	, 1);	//（顶空方式）清洗泵开始工作时刻
            unittypedict.Add( 0x0035	, 1);	//（顶空方式）清洗泵工作时间
            unittypedict.Add( 0x0036	, 1);	//（顶空方式）置换阀开始工作时刻
            unittypedict.Add( 0x0037	, 1);	//（顶空方式）置换阀工作时间
            //检测辅助控制
            unittypedict.Add( 0x003A	, 0);	//（手动）冷井开关
            unittypedict.Add( 0x003B	, 0);	//（手动）传感器室制冷
            unittypedict.Add( 0x003D	, 2);	//传感器室温度实际采样值
            unittypedict.Add( 0x003E	, 2);	//冷井温度实际采样值
            unittypedict.Add( 0x003F	, 2);	//色谱柱温度实际采样值
            unittypedict.Add( 0x0041	, 1);	//冷井启动开始时刻
            unittypedict.Add( 0x0042	, 1);	//冷井连续工作时间
            unittypedict.Add( 0x0043	, 2);	//冷井温度设置值
            unittypedict.Add( 0x0044	, 2);	//冷井温度设置P值
            unittypedict.Add( 0x0045	, 2);	//冷井温度设置I值
            unittypedict.Add( 0x0046	, 2);	//冷井温度设置D值
            unittypedict.Add( 0x0047	, 2);	//冷井温度控制PID范围
            unittypedict.Add( 0x0048	, 2);	//色谱柱温度设置值
            unittypedict.Add( 0x0049	, 2);	//色谱柱温度设置P值
            unittypedict.Add( 0x004A	, 2);	//色谱柱温度设置I值
            unittypedict.Add( 0x004B	, 2);	//色谱柱温度设置D值
            unittypedict.Add( 0x004C	, 2);	//色谱柱温度控制PID范围
            unittypedict.Add( 0x004D	, 1);	//传感器室制冷开始时刻
            unittypedict.Add( 0x004E	, 1);	//传感器室连续工作时间
            unittypedict.Add( 0x004F	, 2);	//传感器室温度设置值
            unittypedict.Add( 0x0050	, 2);	//传感器室温度设置P值
            unittypedict.Add( 0x0051	, 2);	//传感器室温度设置I值
            unittypedict.Add( 0x0052	, 2);	//传感器室温度设置D值
            unittypedict.Add( 0x0053	, 2);	//传感器室温度设置PID范围
            //环境及外围控制检测（已编码）
            unittypedict.Add( 0x0056	, 2);	//油温检测实际值
            unittypedict.Add( 0x0057	, 2);	//柜内温度检测实际值
            unittypedict.Add( 0x0058	, 2);	//柜外温度检测实际值
            unittypedict.Add( 0x0059	, 2);	//载气发生器气泵压力检测实际值
            unittypedict.Add( 0x005B	, 0);	//载气发生器气泵开关
            unittypedict.Add( 0x005C	, 0);	//（手动）柜体空调开关
            unittypedict.Add( 0x005D	, 0);	//伴热带立即加热
            unittypedict.Add( 0x005E	, 0);	//载气发生器排水阀
            unittypedict.Add( 0x0060	, 1);	//（电柜）风扇持续工作时间
            unittypedict.Add( 0x0061	, 1);	//（电柜）风扇停止时间
            unittypedict.Add( 0x0062	, 1);	//柜体空调采样前开启时刻
            unittypedict.Add( 0x0063	, 1);	//柜体空调连续工作时间
            unittypedict.Add( 0x0064	, 1);	//伴热带采样前开始时刻
            unittypedict.Add( 0x0065	, 1);	//伴热带采样前工作时间
            unittypedict.Add( 0x0066	, 1);	//载气发生器排水阀启动时刻
            unittypedict.Add( 0x0067	, 1);	//载气发生器排水阀工作时间
            //采样控制
            unittypedict.Add( 0x006A	, 1);	//采样前吹扫阀工作时间
            unittypedict.Add( 0x006B	, 1);	//定量阀工作时间
            unittypedict.Add( 0x006C	, 1);	//定量阀打开后，吹扫阀延迟打开的时间
            unittypedict.Add( 0x006D	, 1);	//采样结束吹扫阀工作时间
            unittypedict.Add( 0x006E	, 1);	//六组分传感器恢复阀采样前开始时刻
            unittypedict.Add( 0x006F	, 1);	//六组分传感器恢复阀采样前工作时间
            unittypedict.Add( 0x0070	, 1);	//六组分传感器恢复阀采样后开始时刻
            unittypedict.Add( 0x0071	, 1);	//六组分传感器恢复阀采样后工作时间
            unittypedict.Add( 0x0072	, 1);	//微水传感器延时开始加热时刻
            unittypedict.Add( 0x0073	, 1);	//微水传感器采样开始时间
            unittypedict.Add( 0x0074	, 1);	//微水传感器采样间隔
            unittypedict.Add( 0x0075	, 1);	//微水传感器AW的采样点数
            unittypedict.Add( 0x0076	, 1);	//微水传感器T的采样点数
            unittypedict.Add( 0x0077	, 1);	//六组分传感器加热开始时刻
            unittypedict.Add( 0x0078	, 1);	//六组分传感器加热工作时间
            unittypedict.Add( 0x0079	, 1);	//采样结束六组分传感器加热工作时间
            unittypedict.Add( 0x007A	, 1);	//六组分采样间隔,
            unittypedict.Add( 0x007B	, 1);	//六组分采样点数
            unittypedict.Add( 0x007C	, 1);	//CO2传感器加热开始时刻
            unittypedict.Add( 0x007D	, 1);	//CO2传感器工作时间
            unittypedict.Add( 0x007E	, 1);	//CO2采样间隔
            unittypedict.Add( 0x007F	, 1);	//CO2采样点数
            unittypedict.Add( 0x0080	, 1);	//CO2气路切换开始时刻(以6组气体开始采样为准)
            unittypedict.Add( 0x0081	, 1);	//CO2气路工作时间
            unittypedict.Add( 0x0082	, 0);	//标定次数
            unittypedict.Add( 0x0084	, 0);	//标定（立即启动）
            unittypedict.Add( 0x0085	, 0);	//采样（立即启动）／采样手动立即启动
            unittypedict.Add( 0x0086	, 3);	//下次采样时间
            unittypedict.Add( 0x0087	, 1);	//采样间隔
            unittypedict.Add( 0x0089	, 0);	//六组分传感器手动立即启动
            unittypedict.Add( 0x008A	, 0);	//微水传感器加热手动立即启动
            unittypedict.Add( 0x008B	, 0);	//微水传感器手动立即启动
            unittypedict.Add( 0x008C	, 0);	//定量阀手动立即启动
            unittypedict.Add( 0x008D	, 0);	//吹扫阀手动立即启动
            unittypedict.Add( 0x008E	, 0);	//CO2切换阀手动立即启动
            unittypedict.Add( 0x0090	, 2);	//载气压力检测实际值
		    //系统设置
            unittypedict.Add( 0x0093	, 4);	//变电站名
            unittypedict.Add( 0x0094	, 4);	// 软件版本
            unittypedict.Add( 0x0095	, 0);	//（方式选择）支持CO2
            unittypedict.Add( 0x0096	, 0);	//（方式选择）支持微水
            unittypedict.Add( 0x0097	, 0);	//（方式选择）脱气方式
            unittypedict.Add( 0x0099	, 3);	//系统时间
            unittypedict.Add( 0x009A	, 0);	//油色谱设备号
            unittypedict.Add( 0x009B	, 0);	//设备状态（手动/自动方式）
            //unittypedict.Add( 0x009D	, 51);	//指向上位机1 IP    指向上位机1 端口号
            //unittypedict.Add( 0x009E	, 1000);  //串口1波特率串口1数据位    串口1停止位    串口1奇偶校验
            //unittypedict.Add( 0x009F	, 5155);  //网卡1 IP网卡1 端口号    网卡1 子网掩码    网卡1 网关
            //unittypedict.Add( 0x00A0	, 51);	//指向上位机2 IP    指向上位机2 端口号
            //unittypedict.Add( 0x00A1	, 1000);  //串口2波特率  串口2数据位   串口2停止位   串口2奇偶校验"
            //unittypedict.Add( 0x00A2	, 5155);  //网卡2 IP  网卡2 端口号  网卡2 子网掩码  网卡2 网关"
            //数据
            unittypedict.Add( 0x00A5	, 2);	//H2ppm
            unittypedict.Add( 0x00A6	, 2);	//COppm
            unittypedict.Add( 0x00A7	, 2);	//CH4ppm
            unittypedict.Add( 0x00A8	, 2);	//CO2ppm
            unittypedict.Add( 0x00A9	, 2);	//C2H2ppm
            unittypedict.Add( 0x00AA	, 2);	//C2H4ppm
            unittypedict.Add( 0x00AB	, 2);	//C2H6ppm
            unittypedict.Add( 0x00AC	, 2);	//WtrAct
            unittypedict.Add( 0x00AD	, 2);	//Tmp
            unittypedict.Add( 0x00AE	, 2);	//Mst
            unittypedict.Add( 0x00AF	, 2);	//TotHyd
            unittypedict.Add( 0x00B0	, 2);	//GasPres
            //计算参数
            unittypedict.Add( 0x00B3	, 2);	//（变压器参数）油密度
            unittypedict.Add( 0x00B4	, 2);	//（变压器参数）油重（T）
            unittypedict.Add( 0x00B5	, 2);	//电压等级
            unittypedict.Add( 0x00B6	, 8);	//油品系数A油品系数B"
            unittypedict.Add( 0x00B7	, 2);	//海拔高度
            unittypedict.Add( 0x00B9	, 9);	//微水修正参数AW-A 微水修正参数AW-K 微水修正参数AW-B"
            unittypedict.Add( 0x00BA	, 10);	//微水修正参数T-A 微水修正参数T-K   微水修正参数T-B"
            unittypedict.Add( 0x00BC	, 6);	//H2峰顶点可能位置--H2峰顶范围起点--H2峰顶范围结束点/H2峰顶宽度"
            unittypedict.Add( 0x00BD	, 7);	//H2左梯度YMin--H2左梯度XMax--H2右梯度YMin--H2右梯度XMax
            unittypedict.Add( 0x00BE	, 11);	//H2 K1值--H2 K1柱修正系数--H2 K1脱气率修正系数--H2 K1最小面积--H2 K1最大面积--H2 K1离线/在线计算偏差基值"
            unittypedict.Add( 0x00BF	, 11);	//H2 K2值--H2 K2柱修正系数--H2 K2脱气率修正系数--H2 K2最小面积--H2 K2最大面积--H2 K2离线/在线计算偏差基值"
            unittypedict.Add( 0x00C0	, 11);	//H2 K3值--H2 K3柱修正系数--H2 K3脱气率修正系数--H2 K3最小面积--H2 K3最大面积--H2 K3离线/在线计算偏差基值"
            unittypedict.Add( 0x00C1	, 11);	//H2 K4值--H2 K4柱修正系数--H2 K4脱气率修正系数--H2 K4最小面积--H2 K4最大面积--H2 K4离线/在线计算偏差基值"
            unittypedict.Add( 0x00C2	, 11);	//H2 K5值--H2 K5柱修正系数--H2 K5脱气率修正系数--H2 K5最小面积--H2 K5最大面积--H2 K5离线/在线计算偏差基值"
            unittypedict.Add( 0x00C3	, 11);	//H2 K6值--H2 K6柱修正系数--H2 K6脱气率修正系数--H2 K6最小面积--H2 K6最大面积--H2 K6离线/在线计算偏差基值"
            unittypedict.Add( 0x00C4	, 11);	//H2 K7值--H2 K7柱修正系数--H2 K7脱气率修正系数--H2 K7最小面积--H2 K7最大面积--H2 K7离线/在线计算偏差基值"
            unittypedict.Add( 0x00C5	, 11);	//H2 K8值--H2 K8柱修正系数--H2 K8脱气率修正系数--H2 K8最小面积--H2 K8最大面积--H2 K8离线/在线计算偏差基值"
            unittypedict.Add( 0x00C6	, 11);	//H2 K9值--H2 K9柱修正系数--H2 K9脱气率修正系数--H2 K9最小面积--H2 K9最大面积--H2 K9离线/在线计算偏差基值"
            unittypedict.Add( 0x00C7	, 11);	//H2 K10值--H2 K10柱修正系数--H2 K10脱气率修正系数--H2 K10最小面积--H2 K10最大面积--H2 K10离线/在线计算偏差基值"
            unittypedict.Add( 0x00C8	, 11);	//H2 K11值--H2 K11柱修正系数--H2 K11脱气率修正系数--H2 K11最小面积--H2 K11最大面积--H2 K11离线/在线计算偏差基值"
            unittypedict.Add( 0x00C9	, 11);	//H2 K12值--H2 K12柱修正系数--H2 K12脱气率修正系数--H2 K12最小面积--H2 K12最大面积--H2 K12离线/在线计算偏差基值"
            unittypedict.Add( 0x00CB	, 6);	//CO峰顶点可能位置--CO峰顶范围起点--CO峰顶范围结束点--CO峰顶宽度"
            unittypedict.Add( 0x00CC	, 7);	//CO左梯度YMin--CO左梯度XMax--CO右梯度YMin--CO右梯度XMax"
            unittypedict.Add( 0x00CD	, 11);	//CO K1值--CO K1柱修正系数--CO K1脱气率修正系数--CO K1最小面积--CO K1最大面积--CO K1离线/在线计算偏差基值"
            unittypedict.Add( 0x00CE	, 11);	//CO K2值--CO K2柱修正系数--CO K2脱气率修正系数--CO K2最小面积--CO K2最大面积--CO K2离线/在线计算偏差基值"
            unittypedict.Add( 0x00CF	, 11);	//CO K3值--CO K3柱修正系数--CO K3脱气率修正系数--CO K3最小面积--CO K3最大面积--CO K3离线/在线计算偏差基值"
            unittypedict.Add( 0x00D0	, 11);	//CO K4值--CO K4柱修正系数--CO K4脱气率修正系数--CO K4最小面积--CO K4最大面积--CO K4离线/在线计算偏差基值"
            unittypedict.Add( 0x00D1	, 11);	//CO K5值--CO K5柱修正系数--CO K5脱气率修正系数--CO K5最小面积--CO K5最大面积--CO K5离线/在线计算偏差基值"
            unittypedict.Add( 0x00D2	, 11);	//CO K6值--CO K6柱修正系数--CO K6脱气率修正系数--CO K6最小面积--CO K6最大面积--CO K6离线/在线计算偏差基值"
            unittypedict.Add( 0x00D3	, 11);	//CO K7值--CO K7柱修正系数--CO K7脱气率修正系数--CO K7最小面积--CO K7最大面积--CO K7离线/在线计算偏差基值"
            unittypedict.Add( 0x00D4	, 11);	//CO K8值--CO K8柱修正系数--CO K8脱气率修正系数--CO K8最小面积--CO K8最大面积--CO K8离线/在线计算偏差基值"
            unittypedict.Add( 0x00D5	, 11);	//CO K9值--CO K9柱修正系数--CO K9脱气率修正系数--CO K9最小面积--CO K9最大面积--CO K9离线/在线计算偏差基值"
            unittypedict.Add( 0x00D6	, 11);	//CO K10值--CO K10柱修正系数--CO K10脱气率修正系数--CO K10最小面积--CO K10最大面积--CO K10离线/在线计算偏差基值"
            unittypedict.Add( 0x00D7	, 11);	//CO K11值--CO K11柱修正系数--CO K11脱气率修正系数--CO K11最小面积--CO K11最大面积--CO K11离线/在线计算偏差基值"
            unittypedict.Add( 0x00D8	, 11);	//CO K12值--CO K12柱修正系数--CO K12脱气率修正系数--CO K12最小面积--CO K12最大面积--CO K12离线/在线计算偏差基值"
            unittypedict.Add( 0x00DA	, 6);	//CH4峰顶点可能位置--CH4峰顶范围起点--CH4峰顶范围结束点--CH4峰顶宽度
            unittypedict.Add( 0x00DB	, 7);	//CH4左梯度YMin--CH4左梯度XMax--CH4右梯度YMin--CH4右梯度XMax
            unittypedict.Add( 0x00DC	, 11);	//CH4 K1值--CH4 K1柱修正系数--CH4 K1脱气率修正系数--CH4 K1最小面积--CH4 K1最大面积--CH4 K1离线/在线计算偏差基值"
            unittypedict.Add( 0x00DD	, 11);	//CH4 K2值--CH4 K2柱修正系数--CH4 K2脱气率修正系数--CH4 K2最小面积--CH4 K2最大面积--CH4 K2离线/在线计算偏差基值"
            unittypedict.Add( 0x00DE	, 11);	//CH4 K3值--CH4 K3柱修正系数--CH4 K3脱气率修正系数--CH4 K3最小面积--CH4 K3最大面积--CH4 K3离线/在线计算偏差基值"
            unittypedict.Add( 0x00DF	, 11);	//CH4 K4值--CH4 K4柱修正系数--CH4 K4脱气率修正系数--CH4 K4最小面积--CH4 K4最大面积--CH4 K4离线/在线计算偏差基值"
            unittypedict.Add( 0x00E0	, 11);	//CH4 K5值--CH4 K5柱修正系数--CH4 K5脱气率修正系数--CH4 K5最小面积--CH4 K5最大面积--CH4 K5离线/在线计算偏差基值"
            unittypedict.Add( 0x00E1	, 11);	//CH4 K6值--CH4 K6柱修正系数--CH4 K6脱气率修正系数--CH4 K6最小面积--CH4 K6最大面积--CH4 K6离线/在线计算偏差基值"
            unittypedict.Add( 0x00E2	, 11);	//CH4 K7值--CH4 K7柱修正系数--CH4 K7脱气率修正系数--CH4 K7最小面积--CH4 K7最大面积--CH4 K7离线/在线计算偏差基值"
            unittypedict.Add( 0x00E3	, 11);	//CH4 K8值--CH4 K8柱修正系数--CH4 K8脱气率修正系数--CH4 K8最小面积--CH4 K8最大面积--CH4 K8离线/在线计算偏差基值"
            unittypedict.Add( 0x00E4	, 11);	//CH4 K9值--CH4 K9柱修正系数--CH4 K9脱气率修正系数--CH4 K9最小面积--CH4 K9最大面积--CH4 K9离线/在线计算偏差基值"
            unittypedict.Add( 0x00E5	, 11);	//CH4 K10值--CH4 K10柱修正系数--CH4 K10脱气率修正系数--CH4 K10最小面积--CH4 K10最大面积--CH4 K10离线/在线计算偏差基值"
            unittypedict.Add( 0x00E6	, 11);	//CH4 K11值--CH4 K11柱修正系数--CH4 K11脱气率修正系数--CH4 K11最小面积--CH4 K11最大面积--CH4 K11离线/在线计算偏差基值
            unittypedict.Add( 0x00E7	, 11);	//CH4 K12值--CH4 K12柱修正系数--CH4 K12脱气率修正系数--CH4 K12最小面积--CH4 K12最大面积--CH4 K12离线/在线计算偏差基值"
            unittypedict.Add( 0x00E9	, 6);	//C2H2峰顶点可能位置--C2H2峰顶范围起点--C2H2峰顶范围结束点--C2H2峰顶宽度"
            unittypedict.Add( 0x00EA	, 7);	//C2H2左梯度YMin--C2H2左梯度XMax--C2H2右梯度YMin--C2H2右梯度XMax"
            unittypedict.Add( 0x00EB	, 11);	//C2H2 K1值--C2H2 K1柱修正系数--C2H2 K1脱气率修正系数--C2H2 K1最小面积--C2H2 K1最大面积--C2H2 K1离线/在线计算偏差基值"
            unittypedict.Add( 0x00EC	, 11);	//C2H2 K2值--C2H2 K2柱修正系数--C2H2 K2脱气率修正系数--C2H2 K2最小面积--C2H2 K2最大面积--C2H2 K2离线/在线计算偏差基值"
            unittypedict.Add( 0x00ED	, 11);	//C2H2 K3值--C2H2 K3柱修正系数--C2H2 K3脱气率修正系数--C2H2 K3最小面积--C2H2 K3最大面积--C2H2 K3离线/在线计算偏差基值"
            unittypedict.Add( 0x00EE	, 11);	//C2H2 K4值--C2H2 K4柱修正系数--C2H2 K4脱气率修正系数--C2H2 K4最小面积--C2H2 K4最大面积--C2H2 K4离线/在线计算偏差基值"
            unittypedict.Add( 0x00EF	, 11);	//C2H2 K5值--C2H2 K5柱修正系数--C2H2 K5脱气率修正系数--C2H2 K5最小面积--C2H2 K5最大面积--C2H2 K5离线/在线计算偏差基值"-
            unittypedict.Add( 0x00F0	, 11);	//C2H2 K6值--C2H2 K6柱修正系数--C2H2 K6脱气率修正系数--C2H2 K6最小面积--C2H2 K6最大面积--C2H2 K6离线/在线计算偏差基值"
            unittypedict.Add( 0x00F1	, 11);	//C2H2 K7值--C2H2 K7柱修正系数--C2H2 K7脱气率修正系数--C2H2 K7最小面积--C2H2 K7最大面积--C2H2 K7离线/在线计算偏差基值"
            unittypedict.Add( 0x00F2	, 11);	//C2H2 K8值--C2H2 K8柱修正系数--C2H2 K8脱气率修正系数--C2H2 K8最小面积--C2H2 K8最大面积--C2H2 K8离线/在线计算偏差基值"
            unittypedict.Add( 0x00F3	, 11);	//C2H2 K9值--C2H2 K9柱修正系数--C2H2 K9脱气率修正系数--C2H2 K9最小面积--C2H2 K9最大面积--C2H2 K9离线/在线计算偏差基值"
            unittypedict.Add( 0x00F4	, 11);	//C2H2 K10值--C2H2 K10柱修正系数--C2H2 K10脱气率修正系数--C2H2 K10最小面积--C2H2 K10最大面积--C2H2 K10离线/在线计算偏差基值"
            unittypedict.Add( 0x00F5	, 11);	//C2H2 K11值--C2H2 K11柱修正系数--C2H2 K11脱气率修正系数--C2H2 K11最小面积--C2H2 K11最大面积--C2H2 K11离线/在线计算偏差基值"
            unittypedict.Add( 0x00F6	, 11);	//C2H2 K12值--C2H2 K12柱修正系数--C2H2 K12脱气率修正系数--C2H2 K12最小面积--C2H2 K12最大面积--C2H2 K12离线/在线计算偏差基值"
            unittypedict.Add( 0x00F8	, 6);	//C2H4峰顶点可能位置--C2H4峰顶范围起点--C2H4峰顶范围结束点--C2H4峰顶宽度"
            unittypedict.Add( 0x00F9	, 7);	//C2H4左梯度YMin--C2H4左梯度XMax--C2H4右梯度YMin--C2H4右梯度XMax"
            unittypedict.Add( 0x00FA	, 11);	//C2H4 K1值--C2H4 K1柱修正系数--C2H4 K1脱气率修正系数--C2H4 K1最小面积--C2H4 K1最大面积--C2H4 K1离线/在线计算偏差基值"
            unittypedict.Add( 0x00FB	, 11);	//C2H4 K2值--C2H4 K2柱修正系数--C2H4 K2脱气率修正系数--C2H4 K2最小面积--C2H4 K2最大面积--C2H4 K2离线/在线计算偏差基值"
            unittypedict.Add( 0x00FC	, 11);	//C2H4 K3值--C2H4 K3柱修正系数--C2H4 K3脱气率修正系数--C2H4 K3最小面积--C2H4 K3最大面积--C2H4 K3离线/在线计算偏差基值"
            unittypedict.Add( 0x00FD	, 11);	//C2H4 K4值--C2H4 K4柱修正系数--C2H4 K4脱气率修正系数--C2H4 K4最小面积--C2H4 K4最大面积--C2H4 K4离线/在线计算偏差基值"
            unittypedict.Add( 0x00FE	, 11);	//C2H4 K5值--C2H4 K5柱修正系数--C2H4 K5脱气率修正系数--C2H4 K5最小面积--C2H4 K5最大面积--C2H4 K5离线/在线计算偏差基值"
            unittypedict.Add( 0x00FF	, 11);	//C2H4 K6值--C2H4 K6柱修正系数--C2H4 K6脱气率修正系数--C2H4 K6最小面积--C2H4 K6最大面积--C2H4 K6离线/在线计算偏差基值"
            unittypedict.Add( 0x0100	, 11);	//C2H4 K7值--C2H4 K7柱修正系数--C2H4 K7脱气率修正系数--C2H4 K7最小面积--C2H4 K7最大面积--C2H4 K7离线/在线计算偏差基值"
            unittypedict.Add( 0x0101	, 11);	//C2H4 K8值--C2H4 K8柱修正系数--C2H4 K8脱气率修正系数--C2H4 K8最小面积--C2H4 K8最大面积--C2H4 K8离线/在线计算偏差基值"
            unittypedict.Add( 0x0102	, 11);	//C2H4 K9值--C2H4 K9柱修正系数--C2H4 K9脱气率修正系数--C2H4 K9最小面积--C2H4 K9最大面积--C2H4 K9离线/在线计算偏差基值"
            unittypedict.Add( 0x0103	, 11);	//C2H4 K10值--C2H4 K10柱修正系数--C2H4 K10脱气率修正系数--C2H4 K10最小面积--C2H4 K10最大面积--C2H4 K10离线/在线计算偏差基值"
            unittypedict.Add( 0x0104	, 11);	//C2H4 K11值--C2H4 K11柱修正系数--C2H4 K11脱气率修正系数--C2H4 K11最小面积--C2H4 K11最大面积--C2H4 K11离线/在线计算偏差基值"
            unittypedict.Add( 0x0105	, 11);	//C2H4 K12值--C2H4 K12柱修正系数--C2H4 K12脱气率修正系数--C2H4 K12最小面积--C2H4 K12最大面积--C2H4 K12离线/在线计算偏差基值"
            unittypedict.Add( 0x0107	, 6);	//C2H6峰顶点可能位置--C2H6峰顶范围起点--C2H6峰顶范围结束点--C2H6峰顶宽度"
            unittypedict.Add( 0x0108	, 7);	//C2H6左梯度YMin--C2H6左梯度XMax--C2H6右梯度YMin--C2H6右梯度XMax"
            unittypedict.Add( 0x0109	, 11);	//C2H6 K1值--C2H6 K1柱修正系数--C2H6 K1脱气率修正系数--C2H6 K1最小面积--C2H6 K1最大面积--C2H6 K1离线/在线计算偏差基值"
            unittypedict.Add( 0x010A	, 11);	//C2H6 K2值--C2H6 K2柱修正系数--C2H6 K2脱气率修正系数--C2H6 K2最小面积--C2H6 K2最大面积--C2H6 K2离线/在线计算偏差基值"
            unittypedict.Add( 0x010B	, 11);	//C2H6 K3值--C2H6 K3柱修正系数--C2H6 K3脱气率修正系数--C2H6 K3最小面积--C2H6 K3最大面积--C2H6 K3离线/在线计算偏差基值"
            unittypedict.Add( 0x010C	, 11);	//C2H6 K4值--C2H6 K4柱修正系数--C2H6 K4脱气率修正系数--C2H6 K4最小面积--C2H6 K4最大面积--C2H6 K4离线/在线计算偏差基值"
            unittypedict.Add( 0x010D	, 11);	//C2H6 K5值--C2H6 K5柱修正系数--C2H6 K5脱气率修正系数--C2H6 K5最小面积--C2H6 K5最大面积--C2H6 K5离线/在线计算偏差基值"
            unittypedict.Add( 0x010E	, 11);	//C2H6 K6值--C2H6 K6柱修正系数--C2H6 K6脱气率修正系数--C2H6 K6最小面积--C2H6 K6最大面积--C2H6 K6离线/在线计算偏差基值"
            unittypedict.Add( 0x010F	, 11);	//C2H6 K7值--C2H6 K7柱修正系数--C2H6 K7脱气率修正系数--C2H6 K7最小面积--C2H6 K7最大面积--C2H6 K7离线/在线计算偏差基值"
            unittypedict.Add( 0x0110	, 11);	//C2H6 K8值--C2H6 K8柱修正系数--C2H6 K8脱气率修正系数--C2H6 K8最小面积--C2H6 K8最大面积--C2H6 K8离线/在线计算偏差基值"
            unittypedict.Add( 0x0111	, 11);	//C2H6 K9值--C2H6 K9柱修正系数--C2H6 K9脱气率修正系数--C2H6 K9最小面积--C2H6 K9最大面积--C2H6 K9离线/在线计算偏差基值"
            unittypedict.Add( 0x0112	, 11);	//C2H6 K10值--C2H6 K10柱修正系数--C2H6 K10脱气率修正系数--C2H6 K10最小面积--C2H6 K10最大面积--C2H6 K10离线/在线计算偏差基值"
            unittypedict.Add( 0x0113	, 11);	//C2H6 K11值--C2H6 K11柱修正系数--C2H6 K11脱气率修正系数--C2H6 K11最小面积--C2H6 K11最大面积--C2H6 K11离线/在线计算偏差基值"
            unittypedict.Add( 0x0114	, 11);	//C2H6 K12值--C2H6 K12柱修正系数--C2H6 K12脱气率修正系数--C2H6 K12最小面积--C2H6 K12最大面积--C2H6 K12离线/在线计算偏差基值"
            unittypedict.Add( 0x0116	, 5);	//色谱图剔除区间起点色谱图剔除区间结束点"
            unittypedict.Add( 0x0118	, 6);	//CO2峰顶点可能位置--CO2峰顶范围起点--CO2峰顶范围结束点--CO2峰顶宽度"
            unittypedict.Add( 0x0119	, 7);	//CO2左梯度YMin--CO2左梯度XMax--CO2右梯度YMin--CO2右梯度XMax"
            unittypedict.Add( 0x011A	, 11);	//CO2 K值--CO2 K柱修正系数--CO2 K脱气率修正系数--CO2 K最小面积--CO2 K最大面积"
		    //分析及报警
            unittypedict.Add( 0x011D	, 0);	//自动阀值告警开启或关闭
            unittypedict.Add( 0x011E	, 0);	//自动诊断开启或设置
            unittypedict.Add( 0x011F	, 1);	//自动告警诊断功能启用的最小日期间隔
            unittypedict.Add( 0x0120	, 0);	//报警功能设置
            unittypedict.Add( 0x0122	, 2);	//H2气体含量注意值，一级报警
            unittypedict.Add( 0x0123	, 2);	//H2气体含量注意值，二级报警
            unittypedict.Add( 0x0124	, 2);	//CO气体含量注意值，一级报警
            unittypedict.Add( 0x0125	, 2);	//CO气体含量注意值，二级报警
            unittypedict.Add( 0x0126	, 2);	//CH4气体含量注意值，一级报警
            unittypedict.Add( 0x0127	, 2);	//CH4气体含量注意值，二级报警
            unittypedict.Add( 0x0128	, 2);	//C2H4气体含量注意值，一级报警
            unittypedict.Add( 0x0129	, 2);	//C2H4气体含量注意值，二级报警
            unittypedict.Add( 0x012A	, 2);	//C2H6气体含量注意值，一级报警
            unittypedict.Add( 0x012B	, 2);	//C2H6气体含量注意值，二级报警
            unittypedict.Add( 0x012C	, 2);	//C2H2气体含量注意值，一级报警
            unittypedict.Add( 0x012D	, 2);	//C2H2气体含量注意值，二级报警
            unittypedict.Add( 0x012E	, 2);	//CO2气体含量注意值，一级报警
            unittypedict.Add( 0x012F	, 2);	//CO2气体含量注意值，二级报警
            unittypedict.Add( 0x0130	, 2);	//总烃，一级报警
            unittypedict.Add( 0x0131	, 2);	//总烃，二级报警
            unittypedict.Add( 0x0132	, 2);	//微水（PPM），一级报警
            unittypedict.Add( 0x0133	, 2);	//微水（PPM），二级报警
            unittypedict.Add( 0x0134	, 2);	//微水（AW），一级报警
            unittypedict.Add( 0x0135	, 2);	//微水（AW），二级报警
            unittypedict.Add( 0x0136	, 2);	//微水（T），一级报警
            unittypedict.Add( 0x0137	, 2);	//微水（T），二级报警
            unittypedict.Add( 0x0138	, 2);	//H2绝对产气速率注意值ul/天，一级报警
            unittypedict.Add( 0x0139	, 2);	//H2绝对产气速率注意值ul/天，二级报警
            unittypedict.Add( 0x013A	, 2);	//H2相对产气速率注意值%/月，一级报警
            unittypedict.Add( 0x013B	, 2);	//H2相对产气速率注意值%/月，二级报警
            unittypedict.Add( 0x013C	, 2);	//H2报警门限值触发条件ul/L
            unittypedict.Add( 0x013D	, 2);	//H2绝对产气速率触发ul/天
            unittypedict.Add( 0x013E	, 2);	//H2相对产气速率触发%/月
            unittypedict.Add( 0x013F	, 2);	//CO绝对产气速率注意值ul/天，一级报警
            unittypedict.Add( 0x0140	, 2);	//CO绝对产气速率注意值ul/天，二级报警
            unittypedict.Add( 0x0141	, 2);	//CO相对产气速率注意值%/月，一级报警
            unittypedict.Add( 0x0142	, 2);	//CO相对产气速率注意值%/月，二级报警
            unittypedict.Add( 0x0143	, 2);	//CO报警门限值触发条件ul/L
            unittypedict.Add( 0x0144	, 2);	//CO绝对产气速率触发ul/天
            unittypedict.Add( 0x0145	, 2);	//CO相对产气速率触发%/月
            unittypedict.Add( 0x0146	, 2);	//CH4绝对产气速率注意值ul/天，一级报警
            unittypedict.Add( 0x0147	, 2);	//CH4绝对产气速率注意值ul/天，二级报警
            unittypedict.Add( 0x0148	, 2);	//CH4相对产气速率注意值%/月，一级报警
            unittypedict.Add( 0x0149	, 2);	//CH4相对产气速率注意值%/月，二级报警
            unittypedict.Add( 0x014A	, 2);	//CH4报警门限值触发条件ul/L
            unittypedict.Add( 0x014B	, 2);	//CH4绝对产气速率触发ul/天
            unittypedict.Add( 0x014C	, 2);	//CH4相对产气速率触发%/月
            unittypedict.Add( 0x014D	, 2);	//C2H2绝对产气速率注意值ul/天，一级报警
            unittypedict.Add( 0x014E	, 2);	//C2H2绝对产气速率注意值ul/天，二级报警
            unittypedict.Add( 0x014F	, 2);	//C2H2相对产气速率注意值%/月，一级报警
            unittypedict.Add( 0x0150	, 2);	//C2H2相对产气速率注意值%/月，二级报警
            unittypedict.Add( 0x0151	, 2);	//C2H2报警门限值触发条件ul/L
            unittypedict.Add( 0x0152	, 2);	//C2H2绝对产气速率触发ul/天
            unittypedict.Add( 0x0153	, 2);	//C2H2相对产气速率触发%/月
            unittypedict.Add( 0x0154	, 2);	//C2H4绝对产气速率注意值ul/天，一级报警
            unittypedict.Add( 0x0155	, 2);	//C2H4绝对产气速率注意值ul/天，二级报警
            unittypedict.Add( 0x0156	, 2);	//C2H4相对产气速率注意值%/月，一级报警
            unittypedict.Add( 0x0157	, 2);	//C2H4相对产气速率注意值%/月，二级报警
            unittypedict.Add( 0x0158	, 2);	//C2H4报警门限值触发条件ul/L
            unittypedict.Add( 0x0159	, 2);	//C2H4绝对产气速率触发ul/天
            unittypedict.Add( 0x015A	, 2);	//C2H4相对产气速率触发%/月
            unittypedict.Add( 0x015B	, 2);	//C2H6绝对产气速率注意值ul/天，一级报警
            unittypedict.Add( 0x015C	, 2);	//C2H6绝对产气速率注意值ul/天，二级报警
            unittypedict.Add( 0x015D	, 2);	//C2H6相对产气速率注意值%/月，一级报警
            unittypedict.Add( 0x015E	, 2);	//C2H6相对产气速率注意值%/月，二级报警
            unittypedict.Add( 0x015F	, 2);	//C2H6报警门限值触发条件ul/L
            unittypedict.Add( 0x0160	, 2);	//C2H6绝对产气速率触发ul/天
            unittypedict.Add( 0x0161	, 2);	//C2H6相对产气速率触发%/月
            unittypedict.Add( 0x0162	, 2);	//CO2绝对产气速率注意值ul/天，一级报警
            unittypedict.Add( 0x0163	, 2);	//CO2绝对产气速率注意值ul/天，二级报警
            unittypedict.Add( 0x0164	, 2);	//CO2相对产气速率注意值%/月，一级报警
            unittypedict.Add( 0x0165	, 2);	//CO2相对产气速率注意值%/月，二级报警
            unittypedict.Add( 0x0166	, 2);	//CO2报警门限值触发条件ul/L
            unittypedict.Add( 0x0167	, 2);	//CO2绝对产气速率触发ul/天
            unittypedict.Add( 0x0168	, 2);	//CO2相对产气速率触发%/月
            #endregion

            #region 操作单元数据长度字典
            //真空脱气
            unitlendict.Add(3, 2);	    //循环时间查询/设置
            unitlendict.Add(4, 1);	    //抽空次数查询/设置
            unitlendict.Add(5, 1);	    //清洗次数查询/设置
            unitlendict.Add(6, 1);	    //脱气次数查询/设置
            unitlendict.Add(7, 1);	    //置换次数查询/设置
            unitlendict.Add(8, 2);	    //脱气机预计脱气完成时间
            unitlendict.Add(10, 4);	//（脱气机）真空度压力检测值
            unitlendict.Add(11, 4);	//（脱气机）气泵气压检测值
            unitlendict.Add(12, 4);	//（脱气机）油压检测值
            unitlendict.Add(13, 1);	//（脱气机）油杯液位状态
            unitlendict.Add(14, 1);	//（脱气机）气杯液位状态
            unitlendict.Add(15, 1);	//（脱气机）气缸进到位
            unitlendict.Add(16, 1);	//（脱气机）气缸退到位
            unitlendict.Add(17, 1);	//（脱气机）油缸进到位
            unitlendict.Add(18, 1);	//（脱气机）油缸退到位
            unitlendict.Add(20, 1);	//（脱气机）油泵
            unitlendict.Add(21, 4);	//（脱气机）油泵转速控制输出
            unitlendict.Add(22, 1);	//（脱气机）进出油阀
            unitlendict.Add(23, 1);	//（脱气机）YV10阀
            unitlendict.Add(24, 1);	//（脱气机）YV11阀
            unitlendict.Add(25, 1);	//（脱气机）YV12阀
            unitlendict.Add(26, 1);	//（脱气机）YV14阀
            unitlendict.Add(27, 1);	//（脱气机）YV15阀
            unitlendict.Add(28, 1);	//（脱气机）气缸YV4阀
            unitlendict.Add(29, 1);	//（脱气机）气缸YV5阀
            unitlendict.Add(30, 1);	//（脱气机）气缸YV6阀
            unitlendict.Add(31, 1);	//（脱气机）气缸YV7阀
            unitlendict.Add(32, 1);	//（脱气机）气泵
            //膜脱气装置
            unitlendict.Add(35, 2);	//（膜脱气）油泵连续工作时间
            unitlendict.Add(36, 2);	//（膜脱气）排气阀清洗时间
            unitlendict.Add(37, 2);	//（膜脱气）气泵清洗时间
            unitlendict.Add(38, 2);	//（膜脱气）排气阀连续工作时间
            unitlendict.Add(39, 2);	//（膜脱气）气泵连续工作时间
            unitlendict.Add(40, 2);	//（膜脱气）排气阀间隔停止时间
            unitlendict.Add(41, 2);	//（膜脱气）气泵间隔停止时间
            //顶空脱气装置
            unitlendict.Add(44, 1);	//（手动）搅拌开关（立即开始）
            unitlendict.Add(47, 1);	//液位A
            unitlendict.Add(48, 1);	//液位B
            unitlendict.Add(49, 4);	//（顶空方式）搅拌缸检测温度
            unitlendict.Add(50, 2);	//（顶空方式）搅拌开始时刻
            unitlendict.Add(51, 2);	//（顶空方式）搅拌工作时间
            unitlendict.Add(52, 2);	//（顶空方式）清洗泵开始工作时刻
            unitlendict.Add(53, 2);	//（顶空方式）清洗泵工作时间
            unitlendict.Add(54, 2);	//（顶空方式）置换阀开始工作时刻
            unitlendict.Add(55, 2);	//（顶空方式）置换阀工作时间
            //检测辅助控制
            unitlendict.Add(58, 1);	//（手动）冷井开关
            unitlendict.Add(59, 1);	//（手动）传感器室制冷
            unitlendict.Add(61, 4);	//传感器室温度实际采样值
            unitlendict.Add(62, 4);	//冷井温度实际采样值
            unitlendict.Add(63, 4);	//色谱柱温度实际采样值
            unitlendict.Add(65, 2);	//冷井启动开始时刻
            unitlendict.Add(66, 2);	//冷井连续工作时间
            unitlendict.Add(67, 4);	//冷井温度设置值
            unitlendict.Add(68, 4);	//冷井温度设置P值
            unitlendict.Add(69, 4);	//冷井温度设置I值
            unitlendict.Add(70, 4);	//冷井温度设置D值
            unitlendict.Add(71, 4);	//冷井温度控制PID范围
            unitlendict.Add(72, 4);	//色谱柱温度设置值
            unitlendict.Add(73, 4);	//色谱柱温度设置P值
            unitlendict.Add(74, 4);	//色谱柱温度设置I值
            unitlendict.Add(75, 4);	//色谱柱温度设置D值
            unitlendict.Add(76, 4);	//色谱柱温度控制PID范围
            unitlendict.Add(77, 2);	//传感器室制冷开始时刻
            unitlendict.Add(78, 2);	//传感器室连续工作时间
            unitlendict.Add(79, 4);	//传感器室温度设置值
            unitlendict.Add(80, 4);	//传感器室温度设置P值
            unitlendict.Add(81, 4);	//传感器室温度设置I值
            unitlendict.Add(82, 4);	//传感器室温度设置D值
            unitlendict.Add(83, 4);	//传感器室温度设置PID范围
            //环境及外围控制检测（已编码）
            unitlendict.Add(86, 4);	//油温检测实际值
            unitlendict.Add(87, 4);	//柜内温度检测实际值
            unitlendict.Add(88, 4);	//柜外温度检测实际值
            unitlendict.Add(89, 4);	//载气发生器气泵压力检测实际值
            unitlendict.Add(91, 1);	//载气发生器气泵开关
            unitlendict.Add(92, 1);	//（手动）柜体空调开关
            unitlendict.Add(93, 1);	//伴热带立即加热
            unitlendict.Add(94, 1);	//载气发生器排水阀
            unitlendict.Add(96, 2);	//（电柜）风扇持续工作时间
            unitlendict.Add(97, 2);	//（电柜）风扇停止时间
            unitlendict.Add(98, 2);	//柜体空调采样前开启时刻
            unitlendict.Add(99, 2);	//柜体空调连续工作时间
            unitlendict.Add(100, 2);	//伴热带采样前开始时刻
            unitlendict.Add(101, 2);	//伴热带采样前工作时间
            unitlendict.Add(102, 2);	//载气发生器排水阀启动时刻
            unitlendict.Add(103, 2);	//载气发生器排水阀工作时间
            //采样控制
            unitlendict.Add(106, 2);	//采样前吹扫阀工作时间
            unitlendict.Add(107, 2);	//定量阀工作时间
            unitlendict.Add(108, 2);	//定量阀打开后，吹扫阀延迟打开的时间
            unitlendict.Add(109, 2);	//采样结束吹扫阀工作时间
            unitlendict.Add(110, 2);	//六组分传感器恢复阀采样前开始时刻
            unitlendict.Add(111, 2);	//六组分传感器恢复阀采样前工作时间
            unitlendict.Add(112, 2);	//六组分传感器恢复阀采样后开始时刻
            unitlendict.Add(113, 2);	//六组分传感器恢复阀采样后工作时间
            unitlendict.Add(114, 2);	//微水传感器延时开始加热时刻
            unitlendict.Add(115, 2);	//微水传感器采样开始时间
            unitlendict.Add(116, 2);	//微水传感器采样间隔
            unitlendict.Add(117, 2);	//微水传感器AW的采样点数
            unitlendict.Add(118, 2);	//微水传感器T的采样点数
            unitlendict.Add(119, 2);	//六组分传感器加热开始时刻
            unitlendict.Add(120, 2);	//六组分传感器加热工作时间
            unitlendict.Add(121, 2);	//采样结束六组分传感器加热工作时间
            unitlendict.Add(122, 2);	//六组分采样间隔,
            unitlendict.Add(123, 2);	//六组分采样点数
            unitlendict.Add(124, 2);	//CO2传感器加热开始时刻
            unitlendict.Add(125, 2);	//CO2传感器工作时间
            unitlendict.Add(126, 2);	//CO2采样间隔
            unitlendict.Add(127, 2);	//CO2采样点数
            unitlendict.Add(128, 2);	//CO2气路切换开始时刻(以6组气体开始采样为准)
            unitlendict.Add(129, 2);	//CO2气路工作时间
            unitlendict.Add(130, 1);	//标定次数
            unitlendict.Add(132, 1);	//标定（立即启动）
            unitlendict.Add(133, 1);	//采样（立即启动）／采样手动立即启动
            unitlendict.Add(134, 4);	//下次采样时间
            unitlendict.Add(135, 2);	//采样间隔
            unitlendict.Add(137, 1);	//六组分传感器手动立即启动
            unitlendict.Add(138, 1);	//微水传感器加热手动立即启动
            unitlendict.Add(139, 1);	//微水传感器手动立即启动
            unitlendict.Add(140, 1);	//定量阀手动立即启动
            unitlendict.Add(141, 1);	//吹扫阀手动立即启动
            unitlendict.Add(142, 1);	//CO2切换阀手动立即启动
            unitlendict.Add(144, 4);	//载气压力检测实际值
            //系统设置
            unitlendict.Add(147, 80);	//变电站名
            unitlendict.Add(148, 16);	// 软件版本
            unitlendict.Add(149, 1);	//（方式选择）支持CO2
            unitlendict.Add(150, 1);	//（方式选择）支持微水
            unitlendict.Add(151, 1);	//（方式选择）脱气方式
            unitlendict.Add(153, 8);	//系统时间
            unitlendict.Add(154, 1);	//油色谱设备号
            unitlendict.Add(155, 1);	//设备状态（手动/自动方式）
            //unitlendict.Add(157, 6);	//指向上位机1 IP    指向上位机1 端口号
            //unitlendict.Add(158, 5);	//串口1波特率   串口1数据位    串口1停止位    串口1奇偶校验
            //unitlendict.Add(159, 14);	//网卡1 IP  网卡1 端口号    网卡1 子网掩码    网卡1 网关
            //unitlendict.Add(160, 6);	//指向上位机2 IP    指向上位机2 端口号
            //unitlendict.Add(161, 5);	//串口2波特率  串口2数据位   串口2停止位   串口2奇偶校验"
            //unitlendict.Add(162, 14);	//网卡2 IP  网卡2 端口号  网卡2 子网掩码  网卡2 网关"
            //数据
            unitlendict.Add(165, 4);	//H2ppm
            unitlendict.Add(166, 4);	//COppm
            unitlendict.Add(167, 4);	//CH4ppm
            unitlendict.Add(168, 4);	//CO2ppm
            unitlendict.Add(169, 4);	//C2H2ppm
            unitlendict.Add(170, 4);	//C2H4ppm
            unitlendict.Add(171, 4);	//C2H6ppm
            unitlendict.Add(172, 4);	//WtrAct
            unitlendict.Add(173, 4);	//Tmp
            unitlendict.Add(174, 4);	//Mst
            unitlendict.Add(175, 4);	//TotHyd
            unitlendict.Add(176, 4);	//GasPres
            //计算参数
            unitlendict.Add(179, 4);	//（变压器参数）油密度
            unitlendict.Add(180, 4);	//（变压器参数）油重（T）
            unitlendict.Add(181, 4);	//电压等级
            unitlendict.Add(182, 8);	//油品系数A油品系数B"
            unitlendict.Add(183, 4);	//海拔高度
            unitlendict.Add(185, 12);	//微水修正参数AW-A 微水修正参数AW-K 微水修正参数AW-B"
            unitlendict.Add(186, 12);	//微水修正参数T-A 微水修正参数T-K   微水修正参数T-B"
            unitlendict.Add(188, 8);	//H2峰顶点可能位置--H2峰顶范围起点--H2峰顶范围结束点/H2峰顶宽度"
            unitlendict.Add(189, 8);	//H2左梯度YMin--H2左梯度XMax--H2右梯度YMin--H2右梯度XMax
            unitlendict.Add(190, 20);	//H2 K1值--H2 K1柱修正系数--H2 K1脱气率修正系数--H2 K1最小面积--H2 K1最大面积--H2 K1离线/在线计算偏差基值"
            unitlendict.Add(191, 20);	//H2 K2值--H2 K2柱修正系数--H2 K2脱气率修正系数--H2 K2最小面积--H2 K2最大面积--H2 K2离线/在线计算偏差基值"
            unitlendict.Add(192, 20);	//H2 K3值--H2 K3柱修正系数--H2 K3脱气率修正系数--H2 K3最小面积--H2 K3最大面积--H2 K3离线/在线计算偏差基值"
            unitlendict.Add(193, 20);	//H2 K4值--H2 K4柱修正系数--H2 K4脱气率修正系数--H2 K4最小面积--H2 K4最大面积--H2 K4离线/在线计算偏差基值"
            unitlendict.Add(194, 20);	//H2 K5值--H2 K5柱修正系数--H2 K5脱气率修正系数--H2 K5最小面积--H2 K5最大面积--H2 K5离线/在线计算偏差基值"
            unitlendict.Add(195, 20);	//H2 K6值--H2 K6柱修正系数--H2 K6脱气率修正系数--H2 K6最小面积--H2 K6最大面积--H2 K6离线/在线计算偏差基值"
            unitlendict.Add(196, 20);	//H2 K7值--H2 K7柱修正系数--H2 K7脱气率修正系数--H2 K7最小面积--H2 K7最大面积--H2 K7离线/在线计算偏差基值"
            unitlendict.Add(197, 20);	//H2 K8值--H2 K8柱修正系数--H2 K8脱气率修正系数--H2 K8最小面积--H2 K8最大面积--H2 K8离线/在线计算偏差基值"
            unitlendict.Add(198, 20);	//H2 K9值--H2 K9柱修正系数--H2 K9脱气率修正系数--H2 K9最小面积--H2 K9最大面积--H2 K9离线/在线计算偏差基值"
            unitlendict.Add(199, 20);	//H2 K10值--H2 K10柱修正系数--H2 K10脱气率修正系数--H2 K10最小面积--H2 K10最大面积--H2 K10离线/在线计算偏差基值"
            unitlendict.Add(200, 20);	//H2 K11值--H2 K11柱修正系数--H2 K11脱气率修正系数--H2 K11最小面积--H2 K11最大面积--H2 K11离线/在线计算偏差基值"
            unitlendict.Add(201, 20);	//H2 K12值--H2 K12柱修正系数--H2 K12脱气率修正系数--H2 K12最小面积--H2 K12最大面积--H2 K12离线/在线计算偏差基值"
            unitlendict.Add(203, 8);	//CO峰顶点可能位置--CO峰顶范围起点--CO峰顶范围结束点--CO峰顶宽度"
            unitlendict.Add(204, 8);	//CO左梯度YMin--CO左梯度XMax--CO右梯度YMin--CO右梯度XMax"
            unitlendict.Add(205, 20);	//CO K1值--CO K1柱修正系数--CO K1脱气率修正系数--CO K1最小面积--CO K1最大面积--CO K1离线/在线计算偏差基值"
            unitlendict.Add(206, 20);	//CO K2值--CO K2柱修正系数--CO K2脱气率修正系数--CO K2最小面积--CO K2最大面积--CO K2离线/在线计算偏差基值"
            unitlendict.Add(207, 20);	//CO K3值--CO K3柱修正系数--CO K3脱气率修正系数--CO K3最小面积--CO K3最大面积--CO K3离线/在线计算偏差基值"
            unitlendict.Add(208, 20);	//CO K4值--CO K4柱修正系数--CO K4脱气率修正系数--CO K4最小面积--CO K4最大面积--CO K4离线/在线计算偏差基值"
            unitlendict.Add(209, 20);	//CO K5值--CO K5柱修正系数--CO K5脱气率修正系数--CO K5最小面积--CO K5最大面积--CO K5离线/在线计算偏差基值"
            unitlendict.Add(210, 20);	//CO K6值--CO K6柱修正系数--CO K6脱气率修正系数--CO K6最小面积--CO K6最大面积--CO K6离线/在线计算偏差基值"
            unitlendict.Add(211, 20);	//CO K7值--CO K7柱修正系数--CO K7脱气率修正系数--CO K7最小面积--CO K7最大面积--CO K7离线/在线计算偏差基值"
            unitlendict.Add(212, 20);	//CO K8值--CO K8柱修正系数--CO K8脱气率修正系数--CO K8最小面积--CO K8最大面积--CO K8离线/在线计算偏差基值"
            unitlendict.Add(213, 20);	//CO K9值--CO K9柱修正系数--CO K9脱气率修正系数--CO K9最小面积--CO K9最大面积--CO K9离线/在线计算偏差基值"
            unitlendict.Add(214, 20);	//CO K10值--CO K10柱修正系数--CO K10脱气率修正系数--CO K10最小面积--CO K10最大面积--CO K10离线/在线计算偏差基值"
            unitlendict.Add(215, 20);	//CO K11值--CO K11柱修正系数--CO K11脱气率修正系数--CO K11最小面积--CO K11最大面积--CO K11离线/在线计算偏差基值"
            unitlendict.Add(216, 20);	//CO K12值--CO K12柱修正系数--CO K12脱气率修正系数--CO K12最小面积--CO K12最大面积--CO K12离线/在线计算偏差基值"
            unitlendict.Add(218, 8);	//CH4峰顶点可能位置--CH4峰顶范围起点--CH4峰顶范围结束点--CH4峰顶宽度
            unitlendict.Add(219, 8);	//CH4左梯度YMin--CH4左梯度XMax--CH4右梯度YMin--CH4右梯度XMax
            unitlendict.Add(220, 20);	//CH4 K1值--CH4 K1柱修正系数--CH4 K1脱气率修正系数--CH4 K1最小面积--CH4 K1最大面积--CH4 K1离线/在线计算偏差基值"
            unitlendict.Add(221, 20);	//CH4 K2值--CH4 K2柱修正系数--CH4 K2脱气率修正系数--CH4 K2最小面积--CH4 K2最大面积--CH4 K2离线/在线计算偏差基值"
            unitlendict.Add(222, 20);	//CH4 K3值--CH4 K3柱修正系数--CH4 K3脱气率修正系数--CH4 K3最小面积--CH4 K3最大面积--CH4 K3离线/在线计算偏差基值"
            unitlendict.Add(223, 20);	//CH4 K4值--CH4 K4柱修正系数--CH4 K4脱气率修正系数--CH4 K4最小面积--CH4 K4最大面积--CH4 K4离线/在线计算偏差基值"
            unitlendict.Add(224, 20);	//CH4 K5值--CH4 K5柱修正系数--CH4 K5脱气率修正系数--CH4 K5最小面积--CH4 K5最大面积--CH4 K5离线/在线计算偏差基值"
            unitlendict.Add(225, 20);	//CH4 K6值--CH4 K6柱修正系数--CH4 K6脱气率修正系数--CH4 K6最小面积--CH4 K6最大面积--CH4 K6离线/在线计算偏差基值"
            unitlendict.Add(226, 20);	//CH4 K7值--CH4 K7柱修正系数--CH4 K7脱气率修正系数--CH4 K7最小面积--CH4 K7最大面积--CH4 K7离线/在线计算偏差基值"
            unitlendict.Add(227, 20);	//CH4 K8值--CH4 K8柱修正系数--CH4 K8脱气率修正系数--CH4 K8最小面积--CH4 K8最大面积--CH4 K8离线/在线计算偏差基值"
            unitlendict.Add(228, 20);	//CH4 K9值--CH4 K9柱修正系数--CH4 K9脱气率修正系数--CH4 K9最小面积--CH4 K9最大面积--CH4 K9离线/在线计算偏差基值"
            unitlendict.Add(229, 20);	//CH4 K10值--CH4 K10柱修正系数--CH4 K10脱气率修正系数--CH4 K10最小面积--CH4 K10最大面积--CH4 K10离线/在线计算偏差基值"
            unitlendict.Add(230, 20);	//CH4 K11值--CH4 K11柱修正系数--CH4 K11脱气率修正系数--CH4 K11最小面积--CH4 K11最大面积--CH4 K11离线/在线计算偏差基值
            unitlendict.Add(231, 20);	//CH4 K12值--CH4 K12柱修正系数--CH4 K12脱气率修正系数--CH4 K12最小面积--CH4 K12最大面积--CH4 K12离线/在线计算偏差基值"
            unitlendict.Add(233, 8);	//C2H2峰顶点可能位置--C2H2峰顶范围起点--C2H2峰顶范围结束点--C2H2峰顶宽度"
            unitlendict.Add(234, 8);	//C2H2左梯度YMin--C2H2左梯度XMax--C2H2右梯度YMin--C2H2右梯度XMax"
            unitlendict.Add(235, 20);	//C2H2 K1值--C2H2 K1柱修正系数--C2H2 K1脱气率修正系数--C2H2 K1最小面积--C2H2 K1最大面积--C2H2 K1离线/在线计算偏差基值"
            unitlendict.Add(236, 20);	//C2H2 K2值--C2H2 K2柱修正系数--C2H2 K2脱气率修正系数--C2H2 K2最小面积--C2H2 K2最大面积--C2H2 K2离线/在线计算偏差基值"
            unitlendict.Add(237, 20);	//C2H2 K3值--C2H2 K3柱修正系数--C2H2 K3脱气率修正系数--C2H2 K3最小面积--C2H2 K3最大面积--C2H2 K3离线/在线计算偏差基值"
            unitlendict.Add(238, 20);	//C2H2 K4值--C2H2 K4柱修正系数--C2H2 K4脱气率修正系数--C2H2 K4最小面积--C2H2 K4最大面积--C2H2 K4离线/在线计算偏差基值"
            unitlendict.Add(239, 20);	//C2H2 K5值--C2H2 K5柱修正系数--C2H2 K5脱气率修正系数--C2H2 K5最小面积--C2H2 K5最大面积--C2H2 K5离线/在线计算偏差基值"-
            unitlendict.Add(240, 20);	//C2H2 K6值--C2H2 K6柱修正系数--C2H2 K6脱气率修正系数--C2H2 K6最小面积--C2H2 K6最大面积--C2H2 K6离线/在线计算偏差基值"
            unitlendict.Add(241, 20);	//C2H2 K7值--C2H2 K7柱修正系数--C2H2 K7脱气率修正系数--C2H2 K7最小面积--C2H2 K7最大面积--C2H2 K7离线/在线计算偏差基值"
            unitlendict.Add(242, 20);	//C2H2 K8值--C2H2 K8柱修正系数--C2H2 K8脱气率修正系数--C2H2 K8最小面积--C2H2 K8最大面积--C2H2 K8离线/在线计算偏差基值"
            unitlendict.Add(243, 20);	//C2H2 K9值--C2H2 K9柱修正系数--C2H2 K9脱气率修正系数--C2H2 K9最小面积--C2H2 K9最大面积--C2H2 K9离线/在线计算偏差基值"
            unitlendict.Add(244, 20);	//C2H2 K10值--C2H2 K10柱修正系数--C2H2 K10脱气率修正系数--C2H2 K10最小面积--C2H2 K10最大面积--C2H2 K10离线/在线计算偏差基值"
            unitlendict.Add(245, 20);	//C2H2 K11值--C2H2 K11柱修正系数--C2H2 K11脱气率修正系数--C2H2 K11最小面积--C2H2 K11最大面积--C2H2 K11离线/在线计算偏差基值"
            unitlendict.Add(246, 20);	//C2H2 K12值--C2H2 K12柱修正系数--C2H2 K12脱气率修正系数--C2H2 K12最小面积--C2H2 K12最大面积--C2H2 K12离线/在线计算偏差基值"
            unitlendict.Add(248, 8);	//C2H4峰顶点可能位置--C2H4峰顶范围起点--C2H4峰顶范围结束点--C2H4峰顶宽度"
            unitlendict.Add(249, 8);	//C2H4左梯度YMin--C2H4左梯度XMax--C2H4右梯度YMin--C2H4右梯度XMax"
            unitlendict.Add(250, 20);	//C2H4 K1值--C2H4 K1柱修正系数--C2H4 K1脱气率修正系数--C2H4 K1最小面积--C2H4 K1最大面积--C2H4 K1离线/在线计算偏差基值"
            unitlendict.Add(251, 20);	//C2H4 K2值--C2H4 K2柱修正系数--C2H4 K2脱气率修正系数--C2H4 K2最小面积--C2H4 K2最大面积--C2H4 K2离线/在线计算偏差基值"
            unitlendict.Add(252, 20);	//C2H4 K3值--C2H4 K3柱修正系数--C2H4 K3脱气率修正系数--C2H4 K3最小面积--C2H4 K3最大面积--C2H4 K3离线/在线计算偏差基值"
            unitlendict.Add(253, 20);	//C2H4 K4值--C2H4 K4柱修正系数--C2H4 K4脱气率修正系数--C2H4 K4最小面积--C2H4 K4最大面积--C2H4 K4离线/在线计算偏差基值"
            unitlendict.Add(254, 20);	//C2H4 K5值--C2H4 K5柱修正系数--C2H4 K5脱气率修正系数--C2H4 K5最小面积--C2H4 K5最大面积--C2H4 K5离线/在线计算偏差基值"
            unitlendict.Add(255, 20);	//C2H4 K6值--C2H4 K6柱修正系数--C2H4 K6脱气率修正系数--C2H4 K6最小面积--C2H4 K6最大面积--C2H4 K6离线/在线计算偏差基值"
            unitlendict.Add(256, 20);	//C2H4 K7值--C2H4 K7柱修正系数--C2H4 K7脱气率修正系数--C2H4 K7最小面积--C2H4 K7最大面积--C2H4 K7离线/在线计算偏差基值"
            unitlendict.Add(257, 20);	//C2H4 K8值--C2H4 K8柱修正系数--C2H4 K8脱气率修正系数--C2H4 K8最小面积--C2H4 K8最大面积--C2H4 K8离线/在线计算偏差基值"
            unitlendict.Add(258, 20);	//C2H4 K9值--C2H4 K9柱修正系数--C2H4 K9脱气率修正系数--C2H4 K9最小面积--C2H4 K9最大面积--C2H4 K9离线/在线计算偏差基值"
            unitlendict.Add(259, 20);	//C2H4 K10值--C2H4 K10柱修正系数--C2H4 K10脱气率修正系数--C2H4 K10最小面积--C2H4 K10最大面积--C2H4 K10离线/在线计算偏差基值"
            unitlendict.Add(260, 20);	//C2H4 K11值--C2H4 K11柱修正系数--C2H4 K11脱气率修正系数--C2H4 K11最小面积--C2H4 K11最大面积--C2H4 K11离线/在线计算偏差基值"
            unitlendict.Add(261, 20);	//C2H4 K12值--C2H4 K12柱修正系数--C2H4 K12脱气率修正系数--C2H4 K12最小面积--C2H4 K12最大面积--C2H4 K12离线/在线计算偏差基值"
            unitlendict.Add(263, 8);	//C2H6峰顶点可能位置--C2H6峰顶范围起点--C2H6峰顶范围结束点--C2H6峰顶宽度"
            unitlendict.Add(264, 8);	//C2H6左梯度YMin--C2H6左梯度XMax--C2H6右梯度YMin--C2H6右梯度XMax"
            unitlendict.Add(265, 20);	//C2H6 K1值--C2H6 K1柱修正系数--C2H6 K1脱气率修正系数--C2H6 K1最小面积--C2H6 K1最大面积--C2H6 K1离线/在线计算偏差基值"
            unitlendict.Add(266, 20);	//C2H6 K2值--C2H6 K2柱修正系数--C2H6 K2脱气率修正系数--C2H6 K2最小面积--C2H6 K2最大面积--C2H6 K2离线/在线计算偏差基值"
            unitlendict.Add(267, 20);	//C2H6 K3值--C2H6 K3柱修正系数--C2H6 K3脱气率修正系数--C2H6 K3最小面积--C2H6 K3最大面积--C2H6 K3离线/在线计算偏差基值"
            unitlendict.Add(268, 20);	//C2H6 K4值--C2H6 K4柱修正系数--C2H6 K4脱气率修正系数--C2H6 K4最小面积--C2H6 K4最大面积--C2H6 K4离线/在线计算偏差基值"
            unitlendict.Add(269, 20);	//C2H6 K5值--C2H6 K5柱修正系数--C2H6 K5脱气率修正系数--C2H6 K5最小面积--C2H6 K5最大面积--C2H6 K5离线/在线计算偏差基值"
            unitlendict.Add(270, 20);	//C2H6 K6值--C2H6 K6柱修正系数--C2H6 K6脱气率修正系数--C2H6 K6最小面积--C2H6 K6最大面积--C2H6 K6离线/在线计算偏差基值"
            unitlendict.Add(271, 20);	//C2H6 K7值--C2H6 K7柱修正系数--C2H6 K7脱气率修正系数--C2H6 K7最小面积--C2H6 K7最大面积--C2H6 K7离线/在线计算偏差基值"
            unitlendict.Add(272, 20);	//C2H6 K8值--C2H6 K8柱修正系数--C2H6 K8脱气率修正系数--C2H6 K8最小面积--C2H6 K8最大面积--C2H6 K8离线/在线计算偏差基值"
            unitlendict.Add(273, 20);	//C2H6 K9值--C2H6 K9柱修正系数--C2H6 K9脱气率修正系数--C2H6 K9最小面积--C2H6 K9最大面积--C2H6 K9离线/在线计算偏差基值"
            unitlendict.Add(274, 20);	//C2H6 K10值--C2H6 K10柱修正系数--C2H6 K10脱气率修正系数--C2H6 K10最小面积--C2H6 K10最大面积--C2H6 K10离线/在线计算偏差基值"
            unitlendict.Add(275, 20);	//C2H6 K11值--C2H6 K11柱修正系数--C2H6 K11脱气率修正系数--C2H6 K11最小面积--C2H6 K11最大面积--C2H6 K11离线/在线计算偏差基值"
            unitlendict.Add(276, 20);	//C2H6 K12值--C2H6 K12柱修正系数--C2H6 K12脱气率修正系数--C2H6 K12最小面积--C2H6 K12最大面积--C2H6 K12离线/在线计算偏差基值"
            unitlendict.Add(278, 4);	//色谱图剔除区间起点色谱图剔除区间结束点"
            unitlendict.Add(280, 8);	//CO2峰顶点可能位置--CO2峰顶范围起点--CO2峰顶范围结束点--CO2峰顶宽度"
            unitlendict.Add(281, 8);	//CO2左梯度YMin--CO2左梯度XMax--CO2右梯度YMin--CO2右梯度XMax"
            unitlendict.Add(282, 20);	//CO2 K值--CO2 K柱修正系数--CO2 K脱气率修正系数--CO2 K最小面积--CO2 K最大面积"
            //分析及报警
            unitlendict.Add(285, 1);	//自动阀值告警开启或关闭
            unitlendict.Add(286, 1);	//自动诊断开启或设置
            unitlendict.Add(287, 2);	//自动告警诊断功能启用的最小日期间隔
            unitlendict.Add(288, 1);	//报警功能设置
            unitlendict.Add(290, 4);	//H2气体含量注意值，一级报警
            unitlendict.Add(291, 4);	//H2气体含量注意值，二级报警
            unitlendict.Add(292, 4);	//CO气体含量注意值，一级报警
            unitlendict.Add(293, 4);	//CO气体含量注意值，二级报警
            unitlendict.Add(294, 4);	//CH4气体含量注意值，一级报警
            unitlendict.Add(295, 4);	//CH4气体含量注意值，二级报警
            unitlendict.Add(296, 4);	//C2H4气体含量注意值，一级报警
            unitlendict.Add(297, 4);	//C2H4气体含量注意值，二级报警
            unitlendict.Add(298, 4);	//C2H6气体含量注意值，一级报警
            unitlendict.Add(299, 4);	//C2H6气体含量注意值，二级报警
            unitlendict.Add(300, 4);	//C2H2气体含量注意值，一级报警
            unitlendict.Add(301, 4);	//C2H2气体含量注意值，二级报警
            unitlendict.Add(302, 4);	//CO2气体含量注意值，一级报警
            unitlendict.Add(303, 4);	//CO2气体含量注意值，二级报警
            unitlendict.Add(304, 4);	//总烃，一级报警
            unitlendict.Add(305, 4);	//总烃，二级报警
            unitlendict.Add(306, 4);	//微水（PPM），一级报警
            unitlendict.Add(307, 4);	//微水（PPM），二级报警
            unitlendict.Add(308, 4);	//微水（AW），一级报警
            unitlendict.Add(309, 4);	//微水（AW），二级报警
            unitlendict.Add(310, 4);	//微水（T），一级报警
            unitlendict.Add(311, 4);	//微水（T），二级报警
            unitlendict.Add(312, 4);	//H2绝对产气速率注意值ul/天，一级报警
            unitlendict.Add(313, 4);	//H2绝对产气速率注意值ul/天，二级报警
            unitlendict.Add(314, 4);	//H2相对产气速率注意值%/月，一级报警
            unitlendict.Add(315, 4);	//H2相对产气速率注意值%/月，二级报警
            unitlendict.Add(316, 4);	//H2报警门限值触发条件ul/L
            unitlendict.Add(317, 4);	//H2绝对产气速率触发ul/天
            unitlendict.Add(318, 4);	//H2相对产气速率触发%/月
            unitlendict.Add(319, 4);	//CO绝对产气速率注意值ul/天，一级报警
            unitlendict.Add(320, 4);	//CO绝对产气速率注意值ul/天，二级报警
            unitlendict.Add(321, 4);	//CO相对产气速率注意值%/月，一级报警
            unitlendict.Add(322, 4);	//CO相对产气速率注意值%/月，二级报警
            unitlendict.Add(323, 4);	//CO报警门限值触发条件ul/L
            unitlendict.Add(324, 4);	//CO绝对产气速率触发ul/天
            unitlendict.Add(325, 4);	//CO相对产气速率触发%/月
            unitlendict.Add(326, 4);	//CH4绝对产气速率注意值ul/天，一级报警
            unitlendict.Add(327, 4);	//CH4绝对产气速率注意值ul/天，二级报警
            unitlendict.Add(328, 4);	//CH4相对产气速率注意值%/月，一级报警
            unitlendict.Add(329, 4);	//CH4相对产气速率注意值%/月，二级报警
            unitlendict.Add(330, 4);	//CH4报警门限值触发条件ul/L
            unitlendict.Add(331, 4);	//CH4绝对产气速率触发ul/天
            unitlendict.Add(332, 4);	//CH4相对产气速率触发%/月
            unitlendict.Add(333, 4);	//C2H2绝对产气速率注意值ul/天，一级报警
            unitlendict.Add(334, 4);	//C2H2绝对产气速率注意值ul/天，二级报警
            unitlendict.Add(335, 4);	//C2H2相对产气速率注意值%/月，一级报警
            unitlendict.Add(336, 4);	//C2H2相对产气速率注意值%/月，二级报警
            unitlendict.Add(337, 4);	//C2H2报警门限值触发条件ul/L
            unitlendict.Add(338, 4);	//C2H2绝对产气速率触发ul/天
            unitlendict.Add(339, 4);	//C2H2相对产气速率触发%/月
            unitlendict.Add(340, 4);	//C2H4绝对产气速率注意值ul/天，一级报警
            unitlendict.Add(341, 4);	//C2H4绝对产气速率注意值ul/天，二级报警
            unitlendict.Add(342, 4);	//C2H4相对产气速率注意值%/月，一级报警
            unitlendict.Add(343, 4);	//C2H4相对产气速率注意值%/月，二级报警
            unitlendict.Add(344, 4);	//C2H4报警门限值触发条件ul/L
            unitlendict.Add(345, 4);	//C2H4绝对产气速率触发ul/天
            unitlendict.Add(346, 4);	//C2H4相对产气速率触发%/月
            unitlendict.Add(347, 4);	//C2H6绝对产气速率注意值ul/天，一级报警
            unitlendict.Add(348, 4);	//C2H6绝对产气速率注意值ul/天，二级报警
            unitlendict.Add(349, 4);	//C2H6相对产气速率注意值%/月，一级报警
            unitlendict.Add(350, 4);	//C2H6相对产气速率注意值%/月，二级报警
            unitlendict.Add(351, 4);	//C2H6报警门限值触发条件ul/L
            unitlendict.Add(352, 4);	//C2H6绝对产气速率触发ul/天
            unitlendict.Add(353, 4);	//C2H6相对产气速率触发%/月
            unitlendict.Add(354, 4);	//CO2绝对产气速率注意值ul/天，一级报警
            unitlendict.Add(355, 4);	//CO2绝对产气速率注意值ul/天，二级报警
            unitlendict.Add(356, 4);	//CO2相对产气速率注意值%/月，一级报警
            unitlendict.Add(357, 4);	//CO2相对产气速率注意值%/月，二级报警
            unitlendict.Add(358, 4);	//CO2报警门限值触发条件ul/L
            unitlendict.Add(359, 4);	//CO2绝对产气速率触发ul/天
            unitlendict.Add(360, 4);	//CO2相对产气速率触发%/月
            #endregion

            //从数据库读取终端的IP和PORT
        }
    }
}

#region 环境及外围控制检测
    //double OilTemprature;		    //油温	
    //double Temprature_In;		    //柜内温度
    //double Temprature_Out;		//柜外温度
    //float QiBengPres ;  		 	//载气发生器气泵压力检测实际值
    //char QiBengSwitch ;		 	//载气发生器气泵开关
    //char AirControlSwitch; 	 	//（手动）柜体空调开关
    //char BanReDaiStart;		 	//伴热带立即加热
    //char DrainSwitch;		 	    //载气发生器排水阀
    //ushort FengShanKeep_Tick; 	//风扇预吹保持时间(以脱气开始时间为准),5*60 [0-65535] (秒)
    //ushort FengShanLast_Tick ;	//风扇吹尾保持时间(用于换气),5*60	[0-65535]	(秒)？？？？？？？？风扇停止时间？？
    //ushort AirControlStart ;  	//柜体空调采样前开启时刻
    //ushort AirControlWork_Tick ;	//柜体空调连续工作时间
    //ushort BanReDaiStart_T; 		//伴热带采样前开始时刻
    //ushort BanReDaiWork_Tick; 	//伴热带采样前工作时间
    //ushort DrainStart;			//载气发生器排水阀启动时刻
    //ushort DrainWork_Tick;		//载气发生器排水阀工作时间
#endregion