var gulp = require('gulp'),
    plugins = require("gulp-load-plugins")(),
    mainBowerFiles = require("main-bower-files")
//mainBowerFiles


gulp.task('libjs', function() {
    return gulp.src(mainBowerFiles())
        .pipe(plugins.filter(["*.js"]))
        .pipe(plugins.concat('lib.js'))
        .pipe(gulp.dest("wwwroot"))
        .pipe(plugins.rename({
            suffix: ".min"
        }))
        .pipe(plugins.uglify())
        .pipe(gulp.dest("wwwroot"));
});

gulp.task('libcss', function() {
    return gulp.src(mainBowerFiles())
        .pipe(plugins.filter(["*.css"]))
        .pipe(plugins.concat('lib.css'))
        .pipe(gulp.dest("wwwroot"))
        .pipe(plugins.rename({
            suffix: ".min"
        }))
        .pipe(plugins.cssnano())
        .pipe(gulp.dest("wwwroot"));
});


gulp.task('buildjs', function() {
    return gulp.src(["app/src/**/site.js", "app/src/**/*.js"])
        .pipe(plugins.concat('app.js'))
        .pipe(gulp.dest("wwwroot"))
        .pipe(plugins.rename({
            suffix: ".min"
        }))
        .pipe(plugins.uglify())
        .pipe(gulp.dest("wwwroot"));
});



gulp.task('buildcss', function() {
    return gulp.src(["app/css/*"])
        .pipe(plugins.concat('site.js'))
        .pipe(gulp.dest("wwwroot"))
        .pipe(plugins.rename({
            suffix: ".min"
        }))
        .pipe(plugins.cssnano())
        .pipe(gulp.dest("wwwroot"));
});


gulp.task('copyhtml', function() {
    return gulp.src(["app/src/images/*", "app/src/**/*.html"])
        .pipe(gulp.dest("wwwroot"));
});

gulp.task('watch',['default'], function() {
  gulp.watch("app/src/**/*.js", ['buildjs']);
  gulp.watch("app/css/**/*.css", ['buildcss']);
  gulp.watch("app/src/**/*.html", ['copyhtml']);
});

gulp.task('default', ['buildjs', 'copyhtml','buildcss','libcss','libjs'], function() {

});
