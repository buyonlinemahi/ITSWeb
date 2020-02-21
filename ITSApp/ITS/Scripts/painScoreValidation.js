function checkPainScore() 
{
    var a = parseInt((document.getElementById("txtPainScore").value).substring(0, 1));
    var b = parseInt((document.getElementById("txtPainScore").value).substring(2, 5));
    if (a <= b) 
    {        
        if (b - a > 1) 
        {
            alert("Invalid Value - Hint : (7/8, 4/5)");
            document.getElementById("txtPainScore").focus();   
        }        
    }
    else if(a > b)
    {
            alert("Invalid Value - Hint : (7/8, 4/5)");
            document.getElementById("txtPainScore").focus();
    }
}