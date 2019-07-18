/*上下拉回弹效果*/
    $("head").append('<style>.drag_box{overflow:auto}._nothing{position:relative;line-height:45px;text-align:center}._nothing em{display:none}._nothing:before{content:"没有更多了";display:inline-block}._upDrag{position:relative;line-height:45px;text-align:center}._upDrag em{display:none}._upDrag:before{content:"上拉加载更多";display:inline-block}._upDrag:after{content:"↑";display:inline-block;font-size:20px}._refresh{text-align:center;line-height:40px;overflow:hidden}._refresh i{font-size:20px}._refresh:before{display:inline-block;content:"下拉刷新"}._refresh_end{text-align:center}._loading{position:relative;line-height:45px;display:block;animation:1s run infinite;text-align:center;color:inherit}._loading:after{content:"正在加载..."}._loading em{display:inline-block;vertical-align:middle;width:20px;height:20px;position:relative;animation:1s loading infinite;margin-right:5px}._loading em:after{content:"";position:absolute;left:0;top:0;border:3px solid transparent;border-top-color:currentcolor;border-left-color:currentcolor;border-bottom-color:currentcolor;border-radius:50%;-webkit-animation:1s loader-05 linear infinite;animation:1s loader-05 linear infinite;width:100%;height:100%;display:inline-block;box-sizing:border-box}._loading em:before{position:absolute;left:0;top:0;content:"";width:20px;height:20px;display:inline-block;border:3px solid currentcolor;opacity:.2;border-radius:50%;width:100%;height:100%;box-sizing:border-box}@-webkit-keyframes loading{0%{transform:rotate(0deg)}100%{transform:rotate(360deg)}}</style>')
	var json={};
	var drag_tag=false;//是否可以拉动的标识
	function touchstart_func(){
		var e=arguments[0];
	    e.preventDefault();
	    var touch = e.touches[0]; //获取第一个触点
	    var x = touch.pageX; //页面触点X坐标
	    var y = touch.pageY; //页面触点Y坐标
	    //记录触点初始位置
	    startX = x;
	    startY = y;
	}
	function touchmove_func(){
		var e=arguments[0];
	    e.preventDefault();
	    var touch = e.touches[0]; //获取第一个触点
	    var x = touch.pageX; //页面触点X坐标
	    var y = touch.pageY; //页面触点Y坐标
	    endX = x;
	    endY = y;
	    var abs = Math.abs(y - startY);
	    
	    if (abs > 30 && abs < 50) {
	   
	        container.style.cssText = "height:"+$(container).height()+"px;transition:1s cubic-bezier(.1, .57, .1, 1); -webkit-transition: 1s cubic-bezier(.1, .57, .1, 1); -webkit-transform: translate(0px, " + (y - startY) + "px) translateZ(0px);";
	    }
	}
	function touchend_func(){
		var e=arguments[0];
	    e.preventDefault();
	    if (Math.abs(endY - startY) > 10) {
	    	
	        container.style.cssText = "height:"+$(container).height()+"px;transition:300ms cubic-bezier(.1, .57, .1, 1); -webkit-transition: 300ms cubic-bezier(.1, .57, .1, 1);  -webkit-transform: translate(0px,0px) translateZ(0px);";
	    }
	    if (endY - startY > 0) {
	        json.downDragFunc && json.downDragFunc()
	    } else if (endY - startY < 0) {
	        json.upDragFunc && json.upDragFunc()
	    }
	
	}
	
	
	
	
	$.fn.extend({
	    dragBounce: function (options) {
	        json = $.extend({
	            upDragFunc: undefined,//上拉加载回调
	            downDragFunc:undefined,//下拉刷新回调
	            upDragStart:undefined,
	        	downDragStart:undefined,
	        	hasHeight:true,//设定模式，false 为拉伸整体，true则每次需要滑动到底部或顶部才触发回弹效果
	        }, options)
	        container = $(this)[0];
	        var run_tag=false;//是否准许拖动的标识
	        if(json.hasHeight){
	        	$(this).scroll(function(){
	        		var top=$(this).scrollTop();
	        		var hgt=$(this).outerHeight();
	        		var all_hgt=container.scrollHeight;
	        		if(top>=all_hgt-hgt)
	        		{
	        		   bindDrag(container);
	        		}
	
	        	})
	
	        }
	        
	        function bindDrag(container){
		        var startX, startY, endX, endY;
		        container.addEventListener('touchstart', touchstart_func,false);
		        container.addEventListener('touchmove', touchmove_func,false);
		        container.addEventListener('touchend',touchend_func,false);
	        	
	        	
	        	
	        	
	        }
	        
	
	    }
	});
	
	/*上拉刷新，下拉加载*/
	//json参数{data:~~,ajaxFunc:~~}
	$.fn.extend({
		dragFenye:function(json){
			var param=$.extend({
				url:"",
				data:{},
				rows_string:"",//这里为字符串,用以拼接ajax回调返回参数中的VUE刷新变量所需的数组，如res+".data",".data"即为参数
				updateVue:undefined,//这里应为Vue变量字符串，用eval解析为真实的Vue变量并赋值
				method:"post",
				dataType:"json",
				async:false,
				disableUp:false,
	        	disableDown:false,
			},json);
			param.data=$.extend({page:1,rows:10},param.data);
			$(this).wrap("<div class='drag_box'></div>");
			
			var drag_dom=$(this).parent();
	         drag_dom.height($(this).height());
	         drag_dom.append("<div><em></em></div>")
	         drag_dom.prepend("<div></div>")
	
			function get_list(){
				$.ajax({
					url:param.url,
					data:param.data,
					dataType:param.dataType,
					method:param.method,
					async:param.async,
					success:function(res){
					     if(typeof param.updateVue =="string")
					     {
					     	if(eval(param.updateVue+".length")<eval("res"+param.rows_string+".length"))
					     	{
					     		if(eval(param.updateVue+".length")>0)
					     		{
			
					     			   drag_dom[0].removeEventListener('touchstart', touchstart_func,false);
		                               drag_dom[0].removeEventListener('touchmove', touchmove_func,false);
		                               drag_dom[0].removeEventListener('touchend',touchend_func,false);
	
					     		}
					     		eval(param.updateVue+"=res"+param.rows_string);
					     		drag_dom.find(">*:last-child").attr("class","_upDrag");

					     	}else{
					     		drag_dom.find(">*:last-child").attr("class","_nothing");
			     		   		drag_dom[0].removeEventListener('touchstart', touchstart_func,false);
                                drag_dom[0].removeEventListener('touchmove', touchmove_func,false);
                                drag_dom[0].removeEventListener('touchend',touchend_func,false);
					     		drag_dom.unbind("scroll");
					     	}
					     	
					     }
						
					}
	
				})
		
			};
			
			get_list();
			var rows=param.data.rows
			drag_dom.dragBounce({
				upDragFunc:function(){
	                   param.data.rows+=rows;
	                   drag_dom.find(">*:last-child").attr("class","_loading")
					   get_list();
					   
	
				},
	
				
				
				
			})
			
	
		}

	})
//初始化效果例如
//	$(".list").dragFenye({
//  url:~~~,
// 	data:{page:1,rows:10},
// 	rows_string:".data",获取ajax返回数据中所需数组信息的字符串
// 	updateVue:"page_data.list",VUE变量赋值的字符串
//})
	
	

























