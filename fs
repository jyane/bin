#!/bin/bash

# This is a command-line tool to backup or restore files.
#
# Usage:
#     fs command
#
# Command:
#     - backup
#         Backup files from ./ to ./fs-back/.
#     - restore
#         Restore files from ./fs-back/ to ./.

# The MIT License (MIT)
#
# Copyright (c) 2014 Shohei KAMIMORI
#
# Permission is hereby granted, free of charge, to any person obtaining a copy
# of this software and associated documentation files (the "Software"), to deal
# in the Software without restriction, including without limitation the rights
# to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# copies of the Software, and to permit persons to whom the Software is
# furnished to do so, subject to the following conditions:
#
# The above copyright notice and this permission notice shall be included in all
# copies or substantial portions of the Software.
#
# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
# SOFTWARE.

DATE="$(date '+%Y%m%d')"
readonly BACKUP_DIRECTORY="fs-back${DATE}/"

_backup() {
    local option="$1"
    rsync -av ./ ${BACKUP_DIRECTORY} --exclude=${BACKUP_DIRECTORY} ${option}
}

_restore() {
    local option="$1"
    rsync -av ${BACKUP_DIRECTORY} ./ ${option}
}

_create_new_backup() {
    mkdir ${BACKUP_DIRECTORY}
    _backup
}

fs::backup() {
    if [ -e $BACKUP_DIRECTORY ]; then
        echo 'Backup directory is already exists.'
        _backup --dry-run
        read -p 'Overwrite existing backup? [y/N] ' yn
        case "${yn}" in
            [yY]*)
                _backup
                echo 'Overwrote existing backup.';;
            *)
                exit 0;;
        esac
    else
        _create_new_backup
        echo "Created new backup ./${BACKUP_DIRECTORY}">&2
    fi
}

fs::restore() {
    if [ -e $BACKUP_DIRECTORY ]; then
        _restore --dry-run
        read -p 'Restore? [y/N]' yn
        case "${yn}" in
            [yY]*)
                _restore
                echo "Restored from ${BACKUP_DIRECTORY}">&2;;
            *)
                exit 0;;
        esac
    else
        echo 'Backup directory does not exist.'
    fi
}


# for safe.
fs::remove() {
    read -p "Command will execute 'rm -rf ${BACKUP_DIRECTORY}', OK? [y/N] " yn
    case "${yn}" in
        [yY]*)
            rm -rf $BACKUP_DIRECTORY
            echo 'Removed backup files.';;
        *)
            exit 0;;
    esac
}

main() {
    local command="$1"

    if type "fs::${command}" >/dev/null 2>/dev/null; then
        "fs::${command}"
    else
        echo "No such command: ${command}" >&2
        exit 1
    fi
}

if [ "$#" -eq 1 ]; then
    main "$1"
else
    echo 'Arguments error.'
    exit 1
fi
