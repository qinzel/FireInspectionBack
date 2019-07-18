// 格式化日期中的月日
function formatNum(num) {
    return num > 9 ? num : "0" + num;
}

// 获取日期部分
function getDate(dateString) {
    var date = new Date(dateString);
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    return year + "-" + formatNum(month) + "-" + formatNum(day);
}