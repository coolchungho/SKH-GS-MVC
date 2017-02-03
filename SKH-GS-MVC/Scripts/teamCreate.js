angular.module('teamApp', [])
.controller('MainCtrl', [function () {
    var self = this;
    self.teamLeaders = [
    { name: '周燕輝', id: '1' },
    { name: '楊圳隆', id: '2' },
    { name: '徐和志', id: '3' },
    { name: '鄭翠芬', id: '4' },
    { name: '歐陽資明', id: '5' },
    { name: '洪宗義', id: '6' },
    { name: '鄭益和', id: '7' },
    { name: '方躍霖', id: '8' },
    { name: '容菁霞', id: '9' },
    { name: '王凱緯', id: '10' },
    { name: '魏嫈倫', id: '11' },
    { name: '張惟婷', id: '12' }];
    self.Rs = [
        { level: 'R1', name: '黃郁仁' },
        { level: 'R1', name: '陳平' },
        { level: 'R2', name: '張耀仁' },
        { level: 'R1', name: '蔡可威' },
        { level: 'R1', name: '張朋暉' },
        { level: 'R1', name: '歐詠豪' },
        { level: 'R2', name: '張容輔' },
        { level: 'R2', name: '呂冠廷' }];
    self.leader = '';
    self.R = '';
    self.teamId = 0;
    self.upDate = function () {
        var teamLeadersLen = self.teamLeaders.length;
        for (i = 0; i < teamLeadersLen;i++) {
            if(self.teamLeaders[i].name== self.leader )
            {
                self.teamId = self.teamLeaders[i].id;
            }          
           
        }
        
    }
    
}]);