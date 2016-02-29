var gulp = require('gulp'),
    concat = require('gulp-concat')

gulp.task('buildjs',function(){
  return gulp.src(["app/src/**/*.js"])
             .pipe(concat('app.js'))
             .pipe(gulp.dest("wwwroot"));
});

gulp.task('copyhtml',function(){

});

gulp.task('default', ['buildjs','copyhtml'],function() {
  
});