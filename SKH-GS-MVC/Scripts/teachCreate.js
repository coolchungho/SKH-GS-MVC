angular.module('teachApp', [])
.controller('MainCtrl', ['$http', function ($http) {
    var self = this;
    self.meetings = [
        { activity_name: 'Journal Meeting', weekDay: '一', teach_time: '7:30~8:30', location: '7樓討論室' },
        { activity_name: '教學門診', weekDay: '一', teach_time: '上午', location: '教6診' },
        { activity_name: 'GS & ER Combine Meeting', weekDay: '三', teach_time: '7:30~8:30', location: '第四會議室' },
        { activity_name: 'Mortality & Morbidity Conference', weekDay: '三', teach_time: '7:30~8:30', location: '7樓討論室' },
        { activity_name: '超期討論', weekDay: '三', teach_time: '7:30~8:30', location: '7樓討論室' },
        { activity_name: '重大手術討論會', weekDay: '三', teach_time: '7:30~8:30', location: '7樓討論室' }
    ];
    self.class_item = '1';
    $http.get('//10.11.15.210/API/api/ClassValues').then(function (response) {
       self.class_item='2' ;
    }, function (errResponse) {
      self.class_item='5';
    });
    self.teachers = [
        { name: '周燕輝', id: 1 },
        { name: '楊圳隆', id: 2 },
        { name: '徐和志', id: 3 },
        { name: '鄭翠芬', id: 4 },
        { name: '歐陽資明', id: 5 },
        { name: '洪宗義', id: 6 },
   { name: '鄭益和', id: 7 },
    { name: '方躍霖', id: 8 },
    { name: '容菁霞', id: 9 },
    { name: '王凱緯', id: 10 },
    { name: '魏嫈倫', id: 11 },
    { name: '張惟婷', id: 12 }];
    self.weekDay =self.meetings[0].weekDay;
    self.activity_name = self.meetings[0].activity_name;
    self.teach_time = self.meetings[0].teach_time;
    self.location = self.meetings[0].location;
    self.teacher = '';
    self.upDate = function () {  
        for (  i=0; i<=5;i++) {
            if (self.meetings[i].activity_name == self.activity_name) {
                self.weekDay = self.meetings[i].weekDay;
                self.teach_time = self.meetings[i].teach_time;
                self.location = self.meetings[i].location;
            }
                  
        }
    
        
    };

}]);