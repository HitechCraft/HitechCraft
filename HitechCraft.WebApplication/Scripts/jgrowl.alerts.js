//Growl notifications with bootstrap styles
var options = {
    closerTemplate: '<div>Закрыть все</div>',
    closer: false
}

function growlInfo(message, header) {
    this.options.header = header != null ? header : 'Инфо';
    this.options.group = 'alert alert-info';

    $.jGrowl(message, this.options);
}

function growlSuccess(message, header) {

    this.options.header = header != null ? header : 'Успешно';
    this.options.group = 'alert alert-success';

    $.jGrowl(message, options);
}

function growlWarning (message, header) {
    this.options.header = header != null ? header : 'Предупреждение';
    this.options.group = 'alert alert-warning';

    $.jGrowl(message, options);
}

function growlError(message, header) {

    this.options.header = header != null ? header : 'Ошибка';
    this.options.group = 'alert alert-danger';

    $.jGrowl(message, options);
}