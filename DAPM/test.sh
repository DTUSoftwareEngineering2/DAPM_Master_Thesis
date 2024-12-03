services=$(yq docker-compose.yml '.services | keys | .[]' )
          
          for service in $services; do
            # Extract the Dockerfile path from docker-compose.yaml
            dockerfile_path=$(yq e ".services.$service.build.dockerfile" docker-compose.yml)
            
            # If Dockerfile path is not specified, use default "Dockerfile"
            if [ "$dockerfile_path" = "null" ]; then
              dockerfile_path="Dockerfile"
            fi
            
            # Extract the build context from docker-compose.yaml
            build_context=$(yq e ".services.$service.build.context" docker-compose.yml)
            
            # If build context is not specified, use "."
            if [ "$build_context" = "null" ]; then
              build_context="."
            fi
            
            echo "Building $service using $dockerfile_path in context $build_context"
            
            # Build and push the image
          done
