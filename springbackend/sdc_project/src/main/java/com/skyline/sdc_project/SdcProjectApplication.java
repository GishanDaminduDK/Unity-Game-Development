package com.skyline.sdc_project;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.gson.Gson;
import org.modelmapper.ModelMapper;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
public class SdcProjectApplication {

	public static void main(String[] args) {
		SpringApplication.run(SdcProjectApplication.class, args);
	}


	@Bean
	public ObjectMapper mapper(){
		return new ObjectMapper();
	}
	@Bean
	public Gson gson(){
		return new Gson();
	}
	@Bean
	public ModelMapper modelMapper(){
		return new ModelMapper();
	}

}
