var gulp = require('gulp');


gulp.task('buildjs',function(){
  return gulp.src(["app/src/**/*.js"])
             .pipe(gulp.dest("wwwroot"));
});

gulp.task('copyhtml',function(){

});

gulp.task('default', ['buildjs','copyhtml'],function() {
  
});