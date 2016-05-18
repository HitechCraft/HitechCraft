//Growl notifications with bootstrap styles
var options = {
    closerTemplate: '<div>Закрыть все</div>',
    closer: false
}

function growlInfo (message) {
    this.options.header = 'Инфо';
    this.options.group = 'alert alert-info';

    $.jGrowl(message, this.options);
}

function growlSuccess (message) {

    this.options.header = 'Успешно';
    this.options.group = 'alert alert-success';

    $.jGrowl(message, options);
}

function growlWarning (message) {

    this.options.header = 'Предупреждение';
    this.options.group = 'alert alert-warning';

    $.jGrowl(message, options);
}

function growlError (message) {

    this.options.header = 'Ошибка';
    this.options.group = 'alert alert-danger';

    $.jGrowl(message, options);
}