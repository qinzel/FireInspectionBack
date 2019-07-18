
	var com_data=new Vue({
		el:".main",
		data:{
			xxx:{},
			page_data:{}
	
		}

     })
	
	function vue_obj(json){
	          com_data.page_data=json;
		
	}
	Vue.directive('val', function (el, binding) {

	     if(com_data[binding.value])
		{
			$(this.el).text(binding.value)
			
		}else{
			$(this.el).text(com_data.page_data[binding.value]);

		}
	     
	})


