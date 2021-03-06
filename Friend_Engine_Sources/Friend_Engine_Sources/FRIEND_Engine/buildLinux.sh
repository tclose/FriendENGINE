sudo g++ \
main.cpp filefuncs.cpp ../PlugIn/vardb.cpp socket.cxx socket2.cpp parser.cpp intervals.cpp fslfuncs.cpp process.cpp engine.cpp defs.cpp PlugInHandler.cpp  confusionmatrix.cpp session.cpp utils.cpp \
-DUNIX -DLINUX -DEXPOSE_TREACHEROUS -DHAVE_LIBPNG -DHAVE_ZLIB \
-I. \
-I../PlugIn \
-I$FSLDIR/src \
-I$FSLDIR/extras/src/newmat \
-I$FSLDIR/extras/src/libgd \
-I$FSLDIR/extras/src/libgdc \
-I$FSLDIR/extras/src/libpng \
-I$FSLDIR/extras/src/libprob \
-I$FSLDIR/extras/src/zlib \
-I../simpleini \
-L$FSLDIR/extras/lib \
-L$FSLDIR/lib \
-lfslio \
-lnewimage \
-lmiscmaths \
-lcprob \
-lnewmat \
-lniftiio \
-lz \
-lznz \
-lmiscplot \
-lgdc \
-lgd \
-lpng \
-lpthread -ldl -o ../Application/engine 2>&1 | tee erros.txt
