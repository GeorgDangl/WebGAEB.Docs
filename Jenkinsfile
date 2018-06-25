pipeline {
    agent {
        node {
            label 'master'
            customWorkspace 'workspace/WebGaeb.Docs'
        }
    }
    environment {
        DocuApiEndpoint = credentials('docu_api_upload_endpoint')
        DocuApiKey = credentials('WebGAEB.Docs.Docu_ApiKey')
    }
    stages {
        stage ('Deploy') {
            steps {
                powershell './build.ps1 UploadDocumentation'
            }
        }
    }
    post {
        always {
            step([$class: 'Mailer',
                notifyEveryUnstableBuild: true,
                recipients: "georg@dangl.me",
                sendToIndividuals: true])
        }
    }
}
