
function Moneyvalidate(value) {
    var c = value.toString();
    var re = new RegExp("^[0-9]([.,][0-9]{1,3})?$");
    if (re.test(value) == false)
    {
       return "Invalid for money";
    }

}


function ExistCategoryName(value) {
    var t = value.val();
    $.ajax({
        type: "POST",
        url: '/Administrator/Validate/CheckExistCatagoryName/',
        data: {
            Name:t
        },
        traditional: true,
        dataType: 'json',
        complete: function (edata) {
              if(edata.responseText!="")
              {
              return edata.responseText;
              }
            
        }

    });
}