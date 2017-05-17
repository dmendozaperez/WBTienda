//// SELECT SINGLE RADIO BUTTON ONLY.
//function check(objID) {
//    var rbSelEmp = $(document.getElementById(objID));
//    if (rbSelEmp)
//    {
//        alert('ok');
//    }
    
//}

//$('#btView').click(function () {

//    var bSelected = true;
//    var sEmpName;

//    // CHECK EACH ROW FOR THE SELECTED RADIO BUTTON.
//    $('#dgfinanzas').find('input:radio').each(function () {

//        var name = $(this).attr('name');

//        if ($('input:radio[name=' + name + ']:checked').length == 0) {
//            bSelected = false
//        }
//        else {
//            // GET THE EMPLOYEE NAME (3rd COLUMN) FROM THE ROW WHICH IS CHECKED.
//            sEmpName = $('input:radio[name=' + name + ']:checked').closest('tr')
//                .children('td:nth-child(1)').map(function () {

//                    return $.trim($(this).text());
//                }).get();
//        }
//    });

//    // FINALLY SHOW THE MESSAGE.
//    if (bSelected == false) {
//        alert('Invalid Selection'); return false
//    }
//    else
//        alert('Employee Name: ' + sEmpName);
//});


//$(function () {

//    $('input#optsif').click(function () {
//        alert('ok')
//    });
//});