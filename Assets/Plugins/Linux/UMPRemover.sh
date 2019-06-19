#!/bin/sh
 
cd "$(dirname "$0")"
sudo sh -c "rm -f /etc/ld.so.conf.d/ump.conf"
sudo ldconfig
sudo apt-get remove liba52-0.7.4 libaacs0 libass5 libavformat-ffmpeg56 libbasicusageenvironment1 libbdplus0 libbluray1 libcddb2 libchromaprint0 libdc1394-22 libdca0 libdirectfb-1.2-9 libdvbpsi10 libdvdnav4 libdvdread4 libebml4v5 libfaad2 libgles1-mesa libgme0 libgroupsock8 libiso9660-8 libkate1 liblivemedia50 libmad0 libmatroska6v5 libmodplug1 libmpcdec6 libmpeg2-4 libpostproc-ffmpeg53 libproxy-tools libqt5x11extras5 libresid-builder0c2a libsdl-image1.2 libsdl1.2debian libsidplay2v5 libssh-gcrypt-4 libssh2-1 libupnp6 libusageenvironment3 libva-drm1 libva-x11-1 libvcdinfo0 libxcb-composite0 libxcb-xv0 libswscale-ffmpeg3 libavcodec-ffmpeg56
sudo apt-get autoremove
