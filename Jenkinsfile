pipeline {
  agent any
  stages {
    stage('message') {
      steps {
        echo 'hello baby'
      }
    }
    stage('build') {
      steps {
        build(job: 'hunghv2', quietPeriod: 1, wait: true)
      }
    }
    stage('sleeps') {
      steps {
        sleep(unit: 'SECONDS', time: 100)
      }
    }
    stage('end') {
      steps {
        mail(subject: 'abc', body: 'done', bcc: 'hunghvhpu@gmail.com', cc: 'hunghvhpu@gmail.com', from: 'hunghvhpu@gmail.com', to: 'hunghvhpu@gmail.com')
      }
    }
  }
}