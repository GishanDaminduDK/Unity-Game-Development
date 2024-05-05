package com.skyline.sdc_project.config;
import com.skyline.sdc_project.service.PlayerService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpMethod;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configurers.AbstractHttpConfigurer;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;


@Configuration
@EnableWebSecurity
public class SecurityConfig {
    private final JwtAuthorizationFilter jwtAuthorizationFilter;
    private final PlayerService playerService;

    public SecurityConfig(PlayerService playerService, JwtAuthorizationFilter jwtAuthorizationFilter) {
        this.playerService = playerService;
        this.jwtAuthorizationFilter = jwtAuthorizationFilter;

    }

    @Bean
//    public SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {
//        System.out.println("SecurityFilterChain");
//        http.csrf(AbstractHttpConfigurer::disable);
//        http.cors(AbstractHttpConfigurer::disable);
//        http
//                .authorizeHttpRequests(request -> {
//
//                    request.requestMatchers("/api/v1/player/log").permitAll().
//                            requestMatchers("/search").hasAnyAuthority("ADMIN","MANAGER")
//                            .antMatchers(HttpMethod.POST, "/register").permitAll() // Permit access to /register endpoint without authentication
//                            .anyRequest().authenticated();
//                })
//                .addFilterBefore(jwtAuthorizationFilter, UsernamePasswordAuthenticationFilter.class);
//        return http.build();
//    }
    public SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {
        System.out.println("SecurityFilterChain");
        http.csrf(AbstractHttpConfigurer::disable);
        http.cors(AbstractHttpConfigurer::disable);
        http
                .authorizeHttpRequests(request -> {
                    request.requestMatchers("/api/v1/player/log").permitAll()
                            .requestMatchers("/search").hasAnyAuthority("ADMIN", "MANAGER")
                            //.requestMatchers(HttpMethod.POST, "/api/v1/player/savePlayer").permitAll()//
                            //.requestMatchers(HttpMethod.POST, "/api/v1/player/savePlayer").authenticated()//
                            .requestMatchers(HttpMethod.POST, "/api/v1/player/saveAnswers").permitAll()//
                            .requestMatchers(HttpMethod.POST, "/api/v1/player/send_credentials").permitAll()//
                            .anyRequest().permitAll();
                            //.anyRequest().authenticated();
                            //.requestMatchers(HttpMethod.GET, "/api/v1/player/answer/{id}").permitAll()//
                })
                .addFilterBefore(jwtAuthorizationFilter, UsernamePasswordAuthenticationFilter.class);
        return http.build();
    }

}