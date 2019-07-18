var cities = new Object();
cities['北京市'] = new Array({ name: '北京', id: 11 });
cities['上海市'] = new Array({ name: '上海', id: 21 });
cities['天津市'] = new Array({ name: '天津', id: 31 });
cities['重庆市'] = new Array({ name: '重庆', id: 41 });
cities['河北省'] = new Array({ name: '石家庄', id: 51 },{ name: '张家口', id: 52 },{ name: '承德', id: 53 },{ name: '秦皇岛', id: 54 },{ name: '唐山', id: 55 },{ name: '廊坊', id: 56 },{ name: '保定', id: 57 },{ name: '沧州', id: 58 },{ name: '衡水', id: 59 },{ name: '邢台', id: 510 },{ name: '邯郸', id: 511 });
cities['山西省'] = new Array({ name: '太原', id: 61 },{ name: '大同', id: 62 },{ name: '朔州', id: 63 },{ name: '阳泉', id: 64 },{ name: '长治', id: 65 },{ name: '晋城', id: 66 },{ name: '忻州', id: 67 },{ name: '吕梁', id: 68 },{ name: '晋中', id: 69 },{ name: '临汾', id: 610 },{ name: '运城', id: 611 });
cities['内蒙古自治区'] = new Array({ name: '呼和浩特', id: 71 },{ name: '包头', id: 72 },{ name: '乌海', id: 73 },{ name: '赤峰', id: 74 },{ name: '呼伦贝尔盟', id: 75 },{ name: '兴安盟', id: 76 },{ name: '哲里木盟', id: 77 },{ name: '锡林郭勒盟', id: 78 },{ name: '乌兰察布盟', id: 79 },{ name: '鄂尔多斯', id: 710 },{ name: '巴彦淖尔盟', id: 711 },{ name: '阿拉善盟', id: 712 });
cities['辽宁省'] = new Array({ name: '沈阳', id: 81 },{ name: '朝阳', id: 82 },{ name: '阜新', id: 83 },{ name: '铁岭', id: 84 },{ name: '抚顺', id: 85 },{ name: '本溪', id: 86 },{ name: '辽阳', id: 87 },{ name: '鞍山', id: 88 },{ name: '丹东', id: 89 },{ name: '大连', id: 810 },{ name: '营口', id: 811 },{ name: '盘锦', id: 812 },{ name: '锦州', id: 813 },{ name: '葫芦岛', id: 814 });
cities['吉林省'] = new Array({ name: '长春', id: 91 },{ name: '白城', id: 92 },{ name: '松原', id: 93 },{ name: '吉林', id: 94 },{ name: '四平', id: 95 },{ name: '辽源', id: 96 },{ name: '通化', id: 97 },{ name: '白山', id: 98 },{ name: '延边', id: 99 });
cities['黑龙江省'] = new Array({ name: '哈尔滨', id: 1001 },{ name: '齐齐哈尔', id: 1002 },{ name: '黑河', id: 1003 },{ name: '大庆', id: 1004 },{ name: '伊春', id: 1005 },{ name: '鹤岗', id: 1006 },{ name: '佳木斯', id: 1007 },{ name: '双鸭山', id: 1008 },{ name: '七台河', id: 1009 },{ name: '鸡西', id: 1010 },{ name: '牡丹江', id: 1011 },{ name: '绥化', id: 1012 },{ name: '大兴安', id: 1013 });
cities['江苏省'] = new Array({ name: '南京', id: 1101 },{ name: '徐州', id: 1102 },{ name: '连云港', id: 1103 },{ name: '宿迁', id: 1104 },{ name: '淮阴', id: 1105 },{ name: '盐城', id: 1106 },{ name: '扬州', id: 1107 },{ name: '泰州', id: 1108 },{ name: '南通', id: 1109 },{ name: '镇江', id: 1110 },{ name: '常州', id: 1111 },{ name: '无锡', id: 1112 },{ name: '苏州', id: 1113 });
cities['浙江省'] = new Array({ name: '杭州', id: 1201 },{ name: '湖州', id: 1202 },{ name: '嘉兴', id: 1203 },{ name: '舟山', id: 1204 },{ name: '宁波', id: 1205 },{ name: '绍兴', id: 1206 },{ name: '金华', id: 1207 },{ name: '台州', id: 1208 },{ name: '温州', id: 1209 },{ name: '丽水', id: 1210 });
cities['安徽省'] = new Array({ name: '合肥', id: 1301 },{ name: '宿州', id: 1302 },{ name: '淮北', id: 1303 },{ name: '阜阳', id: 1304 },{ name: '蚌埠', id: 1305 },{ name: '淮南', id: 1306 },{ name: '滁州', id: 1307 },{ name: '马鞍山', id: 1308 },{ name: '芜湖', id: 1309 },{ name: '铜陵', id: 1310 },{ name: '安庆', id: 1311 },{ name: '黄山', id: 1312 },{ name: '六安', id: 1313 },{ name: '巢湖', id: 1314 },{ name: '池州', id: 1315 },{ name: '宣城', id: 1316 });
cities['福建省'] = new Array({ name: '福州', id: 1401 },{ name: '南平', id: 1402 },{ name: '三明', id: 1403 },{ name: '莆田', id: 1404 },{ name: '泉州', id: 1405 },{ name: '厦门', id: 1406 },{ name: '漳州', id: 1407 },{ name: '龙岩', id: 1408 },{ name: '宁德', id: 1409 });
cities['江西省'] = new Array({ name: '南昌', id: 1501 },{ name: '九江', id: 1502 },{ name: '景德镇', id: 1503 },{ name: '鹰潭', id: 1504 },{ name: '新余', id: 1505 },{ name: '萍乡', id: 1506 },{ name: '赣州', id: 1507 },{ name: '上饶', id: 1508 },{ name: '抚州', id: 1509 },{ name: '宜春', id: 1510 },{ name: '吉安', id: 1511 });
cities['山东省'] = new Array({ name: '济南', id: 1601 },{ name: '聊城', id: 1602 },{ name: '德州', id: 1603 },{ name: '东营', id: 1604 },{ name: '淄博', id: 1605 },{ name: '潍坊', id: 1606 },{ name: '烟台', id: 1607 },{ name: '威海', id: 1608 },{ name: '青岛', id: 1609 },{ name: '日照', id: 1610 },{ name: '临沂', id: 1611 },{ name: '枣庄', id: 1612 },{ name: '济宁', id: 1613 },{ name: '泰安', id: 1614 },{ name: '莱芜', id: 1615 },{ name: '滨州', id: 1616 },{ name: '菏泽', id: 1617 });
cities['河南省'] = new Array({ name: '郑州', id: 1701 },{ name: '三门峡', id: 1702 },{ name: '洛阳', id: 1703 },{ name: '焦作', id: 1704 },{ name: '新乡', id: 1705 },{ name: '鹤壁', id: 1706 },{ name: '安阳', id: 1707 },{ name: '濮阳', id: 1708 },{ name: '开封', id: 1709 },{ name: '商丘', id: 1710 },{ name: '许昌', id: 1711 },{ name: '漯河', id: 1712 },{ name: '平顶山', id: 1713 },{ name: '南阳', id: 1714 },{ name: '信阳', id: 1715 },{ name: '周口', id: 1716 },{ name: '驻马店', id: 1717 });
cities['湖北省'] = new Array({ name: '武汉', id: 1801 },{ name: '十堰', id: 1802 },{ name: '襄攀', id: 1803 },{ name: '荆门', id: 1804 },{ name: '孝感', id: 1805 },{ name: '黄冈', id: 1806 },{ name: '鄂州', id: 1807 },{ name: '黄石', id: 1808 },{ name: '咸宁', id: 1809 },{ name: '荆州', id: 1810 },{ name: '宜昌', id: 1811 },{ name: '恩施', id: 1812 },{ name: '襄樊', id: 1813 });
cities['湖南省'] = new Array({ name: '长沙', id: 1901 },{ name: '张家界', id: 1902 },{ name: '常德', id: 1903 },{ name: '益阳', id: 1904 },{ name: '岳阳', id: 1905 },{ name: '株洲', id: 1906 },{ name: '湘潭', id: 1907 },{ name: '衡阳', id: 1908 },{ name: '郴州', id: 1909 },{ name: '永州', id: 1910 },{ name: '邵阳', id: 1911 },{ name: '怀化', id: 1912 },{ name: '娄底', id: 1913 },{ name: '湘西', id: 1914 });
cities['广东省'] = new Array({ name: '广州', id: 2001 },{ name: '清远', id: 2002 },{ name: '韶关', id: 2003 },{ name: '河源', id: 2004 },{ name: '梅州', id: 2005 },{ name: '潮州', id: 2006 },{ name: '汕头', id: 2007 },{ name: '揭阳', id: 2008 },{ name: '汕尾', id: 2009 },{ name: '惠州', id: 2010 },{ name: '东莞', id: 2011 },{ name: '深圳', id: 2012 },{ name: '珠海', id: 2013 },{ name: '江门', id: 2014 },{ name: '佛山', id: 2015 },{ name: '肇庆', id: 2016 },{ name: '云浮', id: 2017 },{ name: '阳江', id: 2018 },{ name: '茂名', id: 2019 },{ name: '湛江', id: 2020 });
cities['广西壮族自治区'] = new Array({ name: '南宁', id: 2101 },{ name: '桂林', id: 2102 },{ name: '柳州', id: 2103 },{ name: '梧州', id: 2104 },{ name: '贵港', id: 2105 },{ name: '玉林', id: 2106 },{ name: '钦州', id: 2107 },{ name: '北海', id: 2108 },{ name: '防城港', id: 2109 },{ name: '南宁', id: 2110 },{ name: '百色', id: 2111 },{ name: '河池', id: 2112 },{ name: '柳州', id: 2113 },{ name: '贺州', id: 2114 });
cities['海南省'] = new Array({ name: '海口', id: 2201 },{ name: '三亚', id: 2202 });
cities['四川省'] = new Array({ name: '成都', id: 2301 },{ name: '广元', id: 2302 },{ name: '绵阳', id: 2303 },{ name: '德阳', id: 2304 },{ name: '南充', id: 2305 },{ name: '广安', id: 2306 },{ name: '遂宁', id: 2307 },{ name: '内江', id: 2308 },{ name: '乐山', id: 2309 },{ name: '自贡', id: 2310 },{ name: '泸州', id: 2311 },{ name: '宜宾', id: 2312 },{ name: '攀枝花', id: 2313 },{ name: '巴中', id: 2314 },{ name: '达川', id: 2315 },{ name: '资阳', id: 2316 },{ name: '眉山', id: 2317 },{ name: '雅安', id: 2318 },{ name: '阿坝', id: 2319 },{ name: '甘孜', id: 2320 },{ name: '凉山', id: 2321 });
cities['贵州省'] = new Array({ name: '贵阳', id: 2401 },{ name: '六盘水', id: 2402 },{ name: '遵义', id: 2403 },{ name: '毕节', id: 2404 },{ name: '铜仁', id: 2405 },{ name: '安顺', id: 2406 },{ name: '黔东南', id: 2407 },{ name: '黔南', id: 2408 },{ name: '黔西南', id: 2409 });
cities['云南省'] = new Array({ name: '昆明', id: 2501 },{ name: '曲靖', id: 2502 },{ name: '玉溪', id: 2503 },{ name: '丽江', id: 2504 },{ name: '昭通', id: 2505 },{ name: '思茅', id: 2506 },{ name: '临沧', id: 2507 },{ name: '保山', id: 2508 },{ name: '德宏', id: 2509 },{ name: '怒江', id: 2510 },{ name: '迪庆', id: 2511 },{ name: '大理', id: 2512 },{ name: '楚雄', id: 2513 },{ name: '红河', id: 2514 },{ name: '文山', id: 2515 },{ name: '西双版纳', id: 2516 });
cities['西藏自治区'] = new Array({ name: '拉萨', id: 2601 },{ name: '那曲', id: 2602 },{ name: '昌都', id: 2603 },{ name: '林芝', id: 2604 },{ name: '山南', id: 2605 },{ name: '日喀则', id: 2606 },{ name: '阿里', id: 2607 });
cities['陕西省'] = new Array({ name: '西安', id: 2701 },{ name: '延安', id: 2702 },{ name: '铜川', id: 2703 },{ name: '渭南', id: 2704 },{ name: '咸阳', id: 2705 },{ name: '宝鸡', id: 2706 },{ name: '汉中', id: 2707 },{ name: '榆林', id: 2708 },{ name: '商洛', id: 2709 },{ name: '安康', id: 2710 });
cities['甘肃省'] = new Array({ name: '兰州', id: 2801 },{ name: '嘉峪关', id: 2802 },{ name: '金昌', id: 2803 },{ name: '白银', id: 2804 },{ name: '天水', id: 2805 },{ name: '酒泉', id: 2806 },{ name: '张掖', id: 2807 },{ name: '武威', id: 2808 },{ name: '庆阳', id: 2809 },{ name: '平凉', id: 2810 },{ name: '定西', id: 2811 },{ name: '陇南', id: 2812 },{ name: '临夏', id: 2813 },{ name: '甘南', id: 2814 });
cities['青海省'] = new Array({ name: '西宁', id: 2901 },{ name: '海东', id: 2902 },{ name: '海北', id: 2903 },{ name: '海南', id: 2904 },{ name: '黄南', id: 2905 },{ name: '果洛', id: 2906 },{ name: '玉树', id: 2907 },{ name: '海西', id: 2908 });
cities['宁夏回族自治区'] = new Array({ name: '银川', id: 3001 },{ name: '石嘴山', id: 3002 },{ name: '吴忠', id: 3003 },{ name: '固原', id: 3004 });
cities['新疆维吾尔自治区'] = new Array({ name: '乌鲁木齐', id: 3101 },{ name: '克拉玛依', id: 3102 },{ name: '喀什', id: 3103 },{ name: '阿克苏', id: 3104 },{ name: '和田', id: 3105 },{ name: '吐鲁番', id: 3106 },{ name: '哈密', id: 3107 },{ name: '博尔塔拉', id: 3108 },{ name: '昌吉', id: 3109 },{ name: '巴音郭楞', id: 3110 },{ name: '伊犁', id: 3111 },{ name: '塔城', id: 3112 },{ name: '阿勒泰', id: 3113 });
cities['香港特别行政区'] = new Array({ name: '香港', id: 3201 });
cities['澳门特别行政区'] = new Array({ name: '澳门', id: 3301 });
cities['台湾省'] = new Array({ name: '台北', id: 3401 },{ name: '台南', id: 3402 },{ name: '其他', id: 3403 });

// 获取城市列表
function set_city(province, city) {
	var pv, cv, atr;
	var i, ii;
	pv = province.value;
	if(document.getElementById("v-provinceId") != null) {
	    atr = province.selectedOptions[0].getAttribute("data-index");
	    document.getElementById("v-provinceId").value = atr;
	}
	cv = city.value;
	city.length = 1;
	if(pv == '0') return;
	if(typeof(cities[pv]) == 'undefined') return;
	for(i = 0; i < cities[pv].length; i++) {
		ii = i+1;
		var obj = cities[pv][i];
		city.options[ii] = new Option();
		city.options[ii].text = obj["name"];
		city.options[ii].value = obj["name"];
		city.options[ii].setAttribute("data-index", obj["id"]);
	}
}

// 获取城市属性
function set_city_atr(city) {
    var atr = city.selectedOptions[0].getAttribute("data-index");
    document.getElementById("v-cityId").value = atr;
}

// 省份初始化
function province_init() {
    var content = "";
    content += "<option data-index=\"1\" value=\"北京市\">北京市</option>";
    content += "<option data-index=\"2\" value=\"上海市\">上海市</option>";
    content += "<option data-index=\"3\" value=\"天津市\">天津市</option>";
    content += "<option data-index=\"4\" value=\"重庆市\">重庆市</option>";
    content += "<option data-index=\"5\" value=\"河北省\">河北省</option>";
    content += "<option data-index=\"6\" value=\"山西省\">山西省</option>";
    content += "<option data-index=\"7\" value=\"内蒙古自治区\">内蒙古自治区</option>";
    content += "<option data-index=\"8\" value=\"辽宁省\">辽宁省</option>";
    content += "<option data-index=\"9\" value=\"吉林省\">吉林省</option>";
    content += "<option data-index=\"10\" value=\"黑龙江省\">黑龙江省</option>";
    content += "<option data-index=\"11\" value=\"江苏省\">江苏省</option>";
    content += "<option data-index=\"12\" value=\"浙江省\">浙江省</option>";
    content += "<option data-index=\"13\" value=\"安徽省\">安徽省</option>";
    content += "<option data-index=\"14\" value=\"福建省\">福建省</option>";
    content += "<option data-index=\"15\" value=\"江西省\">江西省</option>";
    content += "<option data-index=\"16\" value=\"山东省\">山东省</option>";
    content += "<option data-index=\"17\" value=\"河南省\">河南省</option>";
    content += "<option data-index=\"18\" value=\"湖北省\">湖北省</option>";
    content += "<option data-index=\"19\" value=\"湖南省\">湖南省</option>";
    content += "<option data-index=\"20\" value=\"广东省\">广东省</option>";
    content += "<option data-index=\"21\" value=\"广西壮族自治区\">广西壮族自治区</option>";
    content += "<option data-index=\"22\" value=\"海南省\">海南省</option>";
    content += "<option data-index=\"23\" value=\"四川省\">四川省</option>";
    content += "<option data-index=\"24\" value=\"贵州省\">贵州省</option>";
    content += "<option data-index=\"25\" value=\"云南省\">云南省</option>";
    content += "<option data-index=\"26\" value=\"西藏自治区\">西藏自治区</option>";
    content += "<option data-index=\"27\" value=\"陕西省\">陕西省</option>";
    content += "<option data-index=\"28\" value=\"甘肃省\">甘肃省</option>";
    content += "<option data-index=\"29\" value=\"青海省\">青海省</option>";
    content += "<option data-index=\"30\" value=\"宁夏回族自治区\">宁夏回族自治区</option>";
    content += "<option data-index=\"31\" value=\"新疆维吾尔自治区\">新疆维吾尔自治区</option>";
    content += "<option data-index=\"32\" value=\"香港特别行政区\">香港特别行政区</option>";
    content += "<option data-index=\"33\" value=\"澳门特别行政区\">澳门特别行政区</option>";
    content += "<option data-index=\"34\" value=\"台湾\">台湾</option>";
    return content;
}