#!/bin/sh

firebase \
        appdistribution:distribute \
        "$INPUT_FILE" \
        --app "$FIREBASE_APPID" \
        --groups "$INPUT_GROUPS" \
        --release-notes-file "$RELEASE_NOTES_FILE"  \
        $( (( $INPUT_DEBUG )) && printf %s '--debug' )
