var gulp = require('gulp'),
    plugins = require("gulp-load-plugins")(),
    es = require("event-stream"),
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
    var jsStream = gulp.src(["app/src/**/site.js", "app/src/**/*.js"])
        .pipe(plugins.wrap("//Filename:<%= file.path %>\n<%= contents %>"))

    var htmlStream = gulp.src(["!app/src/index.html", "app/src/**/*.html"])
        .pipe(plugins.angularTemplatecache({module:'stackLite'}));

    return es.merge(jsStream, htmlStream)
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
        .pipe(plugins.concat('site.css'))
        .pipe(gulp.dest("wwwroot"))
        .pipe(plugins.rename({
            suffix: ".min"
        }))
        .pipe(plugins.cssnano())
        .pipe(gulp.dest("wwwroot"));
});


gulp.task('copyhtml', function() {
    return gulp.src(["app/src/images/*", "app/src/index.html"])
        .pipe(gulp.dest("wwwroot"));
});

gulp.task('watch', ['default'], function() {
    gulp.watch(["app/src/**/*.js","app/src/**/*.html"], ['buildjs']);
    gulp.watch("app/css/**/*.css", ['buildcss']);
    
});

gulp.task('default', ['buildjs', 'copyhtml', 'buildcss', 'libcss', 'libjs'], function() {

});
