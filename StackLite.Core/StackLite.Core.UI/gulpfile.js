var gulp = require('gulp'),
    concat = require('gulp-concat'),
    rename = require('gulp-rename'),
    uglify = require('gulp-uglify')

gulp.task('buildjs', function() {
    return gulp.src(["app/src/**/site.js", "app/src/**/*.js"])
        .pipe(concat('app.js'))
        .pipe(gulp.dest("wwwroot"))
        .pipe(rename({
            suffix: ".min"
        }))
        .pipe(uglify())
        .pipe(gulp.dest("wwwroot"));
});

gulp.task('copyhtml', function() {

});

gulp.task('default', ['buildjs', 'copyhtml'], function() {

});
