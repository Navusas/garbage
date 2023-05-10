#!/bin/bash


###
#Loop through .csproj files, and update all NuGet packages.
###


# Find all .csproj files recursively in the root directory
project_files=$(find . -name "*.csproj")

# Loop through each project file and update its dependencies
for file in $project_files; do
    project_directory=$(realpath "$(dirname "$file")")

    echo "Found $project_directory..."

    # Navigate to the project directory
    pushd "$project_directory" || continue
    echo "Inside $project_directory..."

    # Get a list of outdated packages
    package_references=$(dotnet list package --outdated)

    # Update each package to its latest version
    while read -r line; do
        if [[ $line == *">"* ]]; then
            package=$(echo "$line" | awk '{ print $2 }')
            current_version=$(echo "$line" | awk '{ print $4 }')
            latest_version=$(echo "$line" | awk '{ print $5 }')

            echo "Updating $package from $current_version to $latest_version..."
            dotnet add package "$package" --version "$latest_version"
        fi
    done <<< "$package_references"

    # Return to the root directory
    popd
done
