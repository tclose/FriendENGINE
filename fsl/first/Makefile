# $Id: Makefile,v 1.27 2009/03/24 13:55:27 mark Exp $
include ${FSLCONFDIR}/default.mk

PROJNAME = first

USRINCFLAGS = -I${INC_NEWMAT}

USRLDFLAGS = -L${LIB_NEWMAT} 


LIBS= -lshapeModel -lfirst_lib -lfslvtkio -lmeshclass -lnewimage -lprob -lmiscmaths -lfslio -lniftiio -lznz -lnewmat  -lutils -lz 

FMBLIBS= -lnewimage -lmiscmaths -lfslio -lniftiio -lznz -lnewmat  -lutils -lz 

XFILES= first first_utils first_mult_bcorr

SCRIPTS = run_first run_first_all first_flirt concat_bvars first_roi_slicesdir first_boundary_corr surface_fdr

all: ${XFILES}

first: first.o
	${CXX} ${CXXFLAGS} ${LDFLAGS} -o $@ first.o ${LIBS}

first_utils: first_utils.o
	${CXX} ${CXXFLAGS} ${LDFLAGS} -o $@ first_utils.o ${LIBS}

first_mult_bcorr: first_mult_bcorr.o
	${CXX} ${CXXFLAGS} ${LDFLAGS} -o $@ first_mult_bcorr.o ${FMBLIBS}
