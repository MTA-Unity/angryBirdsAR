#!/bin/bash

# Set the prefix to remove
prefix="8d82b5_Angry_Birds_"

# Loop through all files in the current directory
for file in *; do
    # Check if the file name starts with the prefix
    if [[ "$file" == "${prefix}"* ]]; then
        # Extract the part of the file name after the prefix
        new_name="${file#${prefix}}"

        # Rename the file by removing the prefix
        mv "$file" "$new_name"

        echo "Renamed: $file -> $new_name"
    fi
done

